using RH.Domain.Core.Dtos;

namespace RH.Domain.Dtos
{
    public class OpportunityDto: IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
