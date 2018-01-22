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
    public class CandidateTechCommandHandler :
        IRequestHandler<Commands.CandidateTechInsertCommand, ICommandResult>,
        IRequestHandler<Commands.CandidateTechUpdateCommand, ICommandResult>,
        IRequestHandler<Commands.CandidateTechDeleteCommand, ICommandResult>
    {
        public CandidateTechCommandHandler(IMapper mapper, ICandidateTechRepository repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public IMapper Mapper { get; }
        public ICandidateTechRepository Repository { get; }

        public async Task<ICommandResult> Handle(CandidateTechInsertCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.CandidateTechInsertCommand, CandidateTech>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            result = this.validateUnique(entity);
            if (result.IsFailure) return result;

            await this.Repository.InsertAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(CandidateTechUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.CandidateTechUpdateCommand, CandidateTech>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.Repository.UpdateAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(CandidateTechDeleteCommand request, CancellationToken cancellationToken)
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

        private ICommandResult validate(CandidateTech candidateTech)
        {
            ICommandResult result = new FailureResult();
            if (candidateTech.CandidateId <= 0)
            {
                result.Result = "Id do candidato deve ser informado";
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

        private ICommandResult validateUnique(CandidateTech entity)
        {
            bool exist = this.Repository.ExistSameCandidateAndTech(entity.CandidateId, entity.TechnologyId).Result;

            ICommandResult result = new FailureResult();
            if (exist)
            {
                result.Result = "O candidato já possúi essa tecnologia";
            }

            return result;
        }
    }
}
