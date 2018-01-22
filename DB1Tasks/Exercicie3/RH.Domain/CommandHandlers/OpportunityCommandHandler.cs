using AutoMapper;
using MediatR;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Core.CommandHandlers;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Domain.CommandHandlers
{
    public class OpportunityCommandHandler:
        IRequestHandler<Commands.OpportunityInsertCommand, ICommandResult>,
        IRequestHandler<Commands.OpportunityUpdateCommand, ICommandResult>,
        IRequestHandler<Commands.OpportunityDeleteCommand, ICommandResult>
    {
        public IMapper Mapper { get; }
        public IOpportunityRepository Repository { get; }

        public OpportunityCommandHandler(IMapper mapper, IOpportunityRepository opportunityRepository)
        {
            Mapper = mapper;
            Repository = opportunityRepository;
        }

        public async Task<ICommandResult> Handle(OpportunityInsertCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.OpportunityInsertCommand, Opportunity>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.Repository.InsertAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(OpportunityUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.OpportunityUpdateCommand, Opportunity>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.Repository.UpdateAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(OpportunityDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.Repository.GetAsync(request.Id);

            if (entity == null)
            {
                return new FailureResult { Result = "Oportunidade não encontrada" };
            }

            await this.Repository.DeleteAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        private ICommandResult validate(Opportunity opportunity)
        {
            ICommandResult result = new FailureResult();
            if (string.IsNullOrEmpty(opportunity.Name))
            {
                result.Result = "Nome deve ser informado";
            }

            if (opportunity.Name.Length > 150)
            {
                result.Result = "Nome não pode ser maior que 100 caracteres";
            }

            return result;
        }
    }
}
