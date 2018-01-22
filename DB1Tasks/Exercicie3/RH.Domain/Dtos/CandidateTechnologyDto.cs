using RH.Domain.Core.Dtos;

namespace RH.Domain.Dtos
{
    public class CandidateTechnologyDto: IDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Percentage { get; set; }
    }
}
