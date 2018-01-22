using AutoMapper;
using MediatR;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Core.CommandHandlers;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Domain.CommandHandlers
{
    public class OpportunityCandidateCommandHandler :
        IRequestHandler<Commands.OpportunityCandidateInsertCommand, ICommandResult>,
        IRequestHandler<Commands.OpportunityCandidateUpdateCommand, ICommandResult>,
        IRequestHandler<Commands.OpportunityCandidateDeleteCommand, ICommandResult>
    {
        public OpportunityCandidateCommandHandler(IMapper mapper, IOpportunityCandidateRepository repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public IMapper Mapper { get; }
        public IOpportunityCandidateRepository Repository { get; }

        public async Task<ICommandResult> Handle(OpportunityCandidateInsertCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.OpportunityCandidateInsertCommand, OpportunityCandidate>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            result = this.validateUnique(entity);
            if (result.IsFailure) return result;

            await this.Repository.InsertAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(OpportunityCandidateUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.OpportunityCandidateUpdateCommand, OpportunityCandidate>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.Repository.UpdateAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(OpportunityCandidateDeleteCommand request, CancellationToken cancellationToken)
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

        private ICommandResult validate(OpportunityCandidate candidateTech)
        {
            ICommandResult result = new FailureResult();
            if (candidateTech.OpportunityId <= 0)
            {
                result.Result = "Id da oportunidade deve ser informado";
            }

            if (candidateTech.CandidateId <= 0)
            {
                result.Result = "Id do candidato deve ser informado";
            }

            return result;
        }

        private ICommandResult validateUnique(OpportunityCandidate entity)
        {
            bool exist = this.Repository.ExistSameCandidateAndTech(entity.CandidateId, entity.OpportunityId).Result;

            ICommandResult result = new FailureResult();
            if (exist)
            {
                result.Result = "A oportunidade já possúi essa tecnologia";
            }

            return result;
        }
    }
}
