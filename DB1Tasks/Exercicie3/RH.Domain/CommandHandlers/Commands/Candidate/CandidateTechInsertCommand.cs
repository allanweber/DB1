using MediatR;
using RH.Domain.Core.CommandHandlers;

namespace RH.Domain.CommandHandlers.Commands
{
    public class CandidateTechInsertCommand : IRequest<ICommandResult>
    {
        public int CandidateId { get; set; }

        public int TechnologyId { get; set; }

        public int Percentage { get; set; }
    }
}
