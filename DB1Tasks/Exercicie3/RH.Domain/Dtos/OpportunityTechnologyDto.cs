using RH.Domain.Core.Dtos;

namespace RH.Domain.Dtos
{
    public class OpportunityTechnologyDto : IDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Percentage { get; set; }
    }
}
