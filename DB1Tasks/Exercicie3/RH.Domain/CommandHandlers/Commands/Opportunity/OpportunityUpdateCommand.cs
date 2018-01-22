using MediatR;
using RH.Domain.Core.CommandHandlers;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityUpdateCommand : IRequest<ICommandResult>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
