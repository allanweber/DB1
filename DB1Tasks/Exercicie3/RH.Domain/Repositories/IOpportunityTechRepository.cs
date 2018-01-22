using RH.Domain.Core.Repositories;
using RH.Domain.Entities;
using System.Threading.Tasks;

namespace RH.Domain.Repositories
{
    public interface IOpportunityTechRepository : IRepository<OpportunityTech>
    {
        Task<bool> ExistSameCandidateAndTech(int opportunityId, int techId);
    }
}
