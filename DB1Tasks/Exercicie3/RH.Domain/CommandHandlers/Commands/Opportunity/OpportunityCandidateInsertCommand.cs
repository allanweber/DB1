using MediatR;
using RH.Domain.Core.CommandHandlers;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityCandidateInsertCommand : IRequest<ICommandResult>
    {
        public int OpportunityId { get; set; }

        public int CandidateId { get; set; }
    }
}
