using MediatR;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityTechDeleteCommand: IRequest<ICommandResult>
    {
        public OpportunityTechDeleteCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
