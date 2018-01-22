using MediatR;
using RH.Domain.Core.CommandHandlers;

namespace RH.Domain.CommandHandlers.Commands
{
    public class TechnologyInsertCommand: IRequest<ICommandResult>
    {
        public string Name { get; set; }
    }
}
