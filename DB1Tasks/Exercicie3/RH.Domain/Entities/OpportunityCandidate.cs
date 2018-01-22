using RH.Domain.Core.Entities;

namespace RH.Domain.Entities
{
    public class OpportunityCandidate: BaseEntity
    {
        public int OpportunityId { get; private set; }

        public int CandidateId { get; private set; }

        public Candidate Candidate { get; set; }

        public Opportunity Opportunity { get; set; }
    }
}
