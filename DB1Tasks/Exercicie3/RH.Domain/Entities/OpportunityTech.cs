using RH.Domain.Core.Entities;

namespace RH.Domain.Entities
{
    public class OpportunityTech: BaseEntity
    {
        public int OpportunityId { get; private set; }

        public int TechnologyId { get; private set; }

        public int Percentage { get; private set; }

        public Opportunity Opportunity { get; set; }

        public Technology Technology { get; set; }
    }
}
