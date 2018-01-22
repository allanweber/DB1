using RH.Domain.Core.Entities;
using System.Collections.Generic;

namespace RH.Domain.Entities
{
    public class Opportunity: BaseEntity
    {
        public string Name { get; private set; }
    }
}
