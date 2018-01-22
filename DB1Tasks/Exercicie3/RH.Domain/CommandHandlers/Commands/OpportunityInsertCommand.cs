using MediatR;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityInsertCommand : IRequest<ICommandResult>
    {
        public string Name { get; set; }
    }
}
