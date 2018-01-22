using MediatR;
using RH.Domain.Core.CommandHandlers;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityTechUpdateCommand: IRequest<ICommandResult>
    {
        public int Id { get; set; }

        public int OpportunityId { get; set; }

        public int TechnologyId { get; set; }

        public int Percentage { get; set; }
    }
}
