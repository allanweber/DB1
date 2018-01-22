using RH.Domain.Core.Dtos;

namespace RH.Domain.Dtos
{
    public class OpportunityTechDto: IDto
    {
        public int Id { get; set; }

        public int OpportunityId { get; set; }

        public int TechnologyId { get; set; }

        public int Percentage { get; set; }

        public string OpportunityName { get; set; }

        public string TechnologyName { get; set; }
    }
}
