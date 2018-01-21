using RH.Domain.Core.Entities;

namespace RH.Domain.Entities
{
    public class CandidateTech: BaseEntity
    {
        public int CandidateId { get; private set; }

        public int TechnologyId { get; private set; }

        public Candidate Candidate { get; private set; }

        public Technology Technology { get; private set; }
    }
}
