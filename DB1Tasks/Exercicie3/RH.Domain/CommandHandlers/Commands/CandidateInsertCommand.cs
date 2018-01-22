using MediatR;

namespace RH.Domain.CommandHandlers.Commands
{
    public class CandidateInsertCommand: IRequest<ICommandResult>
    {
        public string Name { get; set; }
    }
}
