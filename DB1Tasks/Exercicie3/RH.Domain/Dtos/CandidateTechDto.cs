using RH.Domain.Core.Dtos;

namespace RH.Domain.Dtos
{
    public class CandidateTechDto : IDto
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int TechnologyId { get; set; }

        public string TechnologyName { get; set; }

        public string CandidateName { get; set; }
    }
}
