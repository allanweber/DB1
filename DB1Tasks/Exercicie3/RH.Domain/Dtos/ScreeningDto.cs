using RH.Domain.Core.Dtos;
using System.Collections.Generic;

namespace RH.Domain.Dtos
{
    public class ScreeningDto: IDto
    {
        public ScreeningDto()
        {
            this.Candidates = new List<CandidateScreeningDto>();
        }

        public OpportunityScreeningDto Opportunity { get; set; }

        public IList<CandidateScreeningDto> Candidates { get; set; }
    }
}
