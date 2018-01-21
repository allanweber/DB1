using RH.Domain.Core.Dtos;

namespace RH.Domain.Dtos
{
    public class CandidateTechInsertDto: IDto
    {
        public int CandidateId { get; set; }

        public int TechnologyId { get; set; }
    }
}
