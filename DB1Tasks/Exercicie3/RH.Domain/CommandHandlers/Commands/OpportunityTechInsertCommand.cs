using MediatR;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityTechInsertCommand: IRequest<ICommandResult>
    {
        public int OpportunityId { get; set; }

        public int TechnologyId { get; set; }

        public int Percentage { get; set; }
    }
}
