using RH.Domain.Core.Dtos;

namespace RH.Domain.Dtos
{
    public class CandidateTechUpdateDto: IDto
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int TechnologyId { get; set; }
    }
}
