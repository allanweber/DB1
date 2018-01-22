using RH.Domain.Core.Dtos;
using System.Collections.Generic;

namespace RH.Domain.Dtos
{
    public class OpportunityScreeningDto: IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<OpportunityTechnologyDto> Technologies { get; set; }
    }
}
