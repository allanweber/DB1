using RH.Domain.Core.Dtos;
using System.Collections.Generic;

namespace RH.Domain.Dtos
{
    public class CandidateScreeningDto : IDto
    {
        public CandidateScreeningDto()
        {
            this.Technologies = new List<CandidateTechnologyDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<CandidateTechnologyDto> Technologies { get; set; }
    }
}
