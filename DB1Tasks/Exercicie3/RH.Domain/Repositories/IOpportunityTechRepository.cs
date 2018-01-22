using RH.Domain.Core.Repositories;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Domain.Repositories
{
    public interface IOpportunityTechRepository : IRepository<OpportunityTech>
    {
        Task<bool> ExistSameCandidateAndTech(int opportunityId, int techId);

        Task<List<OpportunityTechnologyDto>> GetAllTechnologyByOpportunity(int opportunityId);
    }
}
