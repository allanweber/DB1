using MediatR;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityDeleteCommand: IRequest<ICommandResult>
    {
        public OpportunityDeleteCommand(int id)
        {
            this.Id = id;
        }
        public int Id { get; set; }
    }
}
