using AutoMapper;
using MediatR;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Domain.CommandHandlers
{
    public class TechnologyCommandHandler:
        IRequestHandler<Commands.TechnologyInsertCommand, ICommandResult>,
        IRequestHandler<Commands.TechnologyUpdateCommand, ICommandResult>,
        IRequestHandler<Commands.TechnologyDeleteCommand, ICommandResult>
    {
        public TechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository)
        {
            Mapper = mapper;
            Repository = technologyRepository;
        }

        public IMapper Mapper { get; }
        public ITechnologyRepository Repository { get; }

        public async Task<ICommandResult> Handle(TechnologyInsertCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.TechnologyInsertCommand, Technology>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.Repository.InsertAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(TechnologyUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.TechnologyUpdateCommand, Technology>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.Repository.UpdateAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(TechnologyDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.Repository.GetAsync(request.Id);

            if (entity == null)
            {
                return new FailureResult { Result = "Tecnologia não encontrada" };
            }

            await this.Repository.DeleteAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        private ICommandResult validate(Technology candidate)
        {
            ICommandResult result = new FailureResult();
            if (string.IsNullOrEmpty(candidate.Name))
            {
                result.Result = "Nome deve ser informado";
            }

            if (candidate.Name.Length > 150)
            {
                result.Result = "Nome não pode ser maior que 100 caracteres";
            }

            return result;
        }
    }

}
