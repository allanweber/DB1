using AutoMapper;
using MediatR;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Domain.CommandHandlers
{
    public class CandidateCommandHandler :
        IRequestHandler<Commands.CandidateInsertCommand, ICommandResult>,
        IRequestHandler<Commands.CandidateUpdateCommand, ICommandResult>,
        IRequestHandler<Commands.CandidateDeleteCommand, ICommandResult>
    {
        public CandidateCommandHandler(IMapper mapper, ICandidateRepository candidateRepository)
        {
            Mapper = mapper;
            Repository = candidateRepository;
        }

        public IMapper Mapper { get; }
        public ICandidateRepository Repository { get; }

        public async Task<ICommandResult> Handle(Commands.CandidateInsertCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.CandidateInsertCommand, Candidate>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.Repository.InsertAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(CandidateUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<Commands.CandidateUpdateCommand, Candidate>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.Repository.UpdateAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(CandidateDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.Repository.GetAsync(request.Id);

            if (entity == null)
            {
                return new FailureResult { Result = "Candidato não encontrado" };
            }

            await this.Repository.DeleteAsync(entity);

            await this.Repository.CommitAsync();

            return new SuccessResult();
        }

        private ICommandResult validate(Candidate candidate)
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
