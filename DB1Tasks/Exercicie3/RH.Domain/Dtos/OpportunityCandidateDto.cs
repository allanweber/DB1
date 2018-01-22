using RH.Domain.Core.Dtos;

namespace RH.Domain.Dtos
{
    public class OpportunityCandidateDto: IDto
    {
        public int Id { get; set; }

        public int OpportunityId { get; set; }

        public int CandidateId { get; set; }

        public string CandidateName { get; set; }

        public string OpportunityName { get; set; }
    }
}
