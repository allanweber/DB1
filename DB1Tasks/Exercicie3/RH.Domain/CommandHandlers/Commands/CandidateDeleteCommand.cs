using MediatR;

namespace RH.Domain.CommandHandlers.Commands
{
    public class CandidateDeleteCommand : IRequest<ICommandResult>
    {
        public CandidateDeleteCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
