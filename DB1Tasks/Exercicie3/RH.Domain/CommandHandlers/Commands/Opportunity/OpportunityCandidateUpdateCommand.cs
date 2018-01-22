using MediatR;
using RH.Domain.Core.CommandHandlers;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityCandidateUpdateCommand : IRequest<ICommandResult>
    {
        public int Id { get; set; }

        public int OpportunityId { get; set; }

        public int CandidateId { get; set; }
    }
}
