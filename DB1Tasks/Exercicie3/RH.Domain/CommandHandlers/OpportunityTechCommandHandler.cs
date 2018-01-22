using AutoMapper;
using MediatR;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Domain.CommandHandlers
{
    public class OpportunityTechCommandHandler :
        IRequestHandler<Commands.OpportunityTechInsertCommand, ICommandResult>,
        IRequestHandler<Commands.OpportunityTechUpdateCommand, ICommandResult>,
        IRequestHandler<Commands.OpportunityTechDeleteCommand, ICommandResult>
    {
        public OpportunityTechCommandHandler(IMapper mapper, IOpportunityTechRepository repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public IMapper Mapper { get; }
        public IOpportunityTechRepository Repository { get; }

        public async Task<ICommandResult> Handle(OpportunityTechInsertCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.OpportunityTechInsertCommand, OpportunityTech>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            result = this.validateUnique(entity);
            if (result.IsFailure) return result;

            await this.Repository.InsertAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(OpportunityTechUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.OpportunityTechUpdateCommand, OpportunityTech>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.Repository.UpdateAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(OpportunityTechDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.Repository.GetAsync(request.Id);

            if (entity == null)
            {
                return new FailureResult { Result = "Tecnologia do candidato não encontrado" };
            }

            await this.Repository.DeleteAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        private ICommandResult validate(OpportunityTech candidateTech)
        {
            ICommandResult result = new FailureResult();
            if (candidateTech.OpportunityId <= 0)
            {
                result.Result = "Id da oportunidade deve ser informado";
            }

            if (candidateTech.TechnologyId <= 0)
            {
                result.Result = "Id da tecnologia deve ser informado";
            }

            if (candidateTech.Percentage <= 0 || candidateTech.Percentage > 100)
            {
                result.Result = "Percentual de conhecimento deve ser entre 0 e 100";
            }

            return result;
        }

        private ICommandResult validateUnique(OpportunityTech entity)
        {
            bool exist = this.Repository.ExistSameCandidateAndTech(entity.OpportunityId, entity.TechnologyId).Result;

            ICommandResult result = new FailureResult();
            if (exist)
            {
                result.Result = "A oportunidade já possúi essa tecnologia";
            }

            return result;
        }
    }
}
