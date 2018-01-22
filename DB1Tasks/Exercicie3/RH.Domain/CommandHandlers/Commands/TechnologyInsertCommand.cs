using MediatR;

namespace RH.Domain.CommandHandlers.Commands
{
    public class TechnologyInsertCommand: IRequest<ICommandResult>
    {
        public string Name { get; set; }
    }
}
