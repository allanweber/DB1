using RH.Domain.Core.Repositories;
using RH.Domain.Entities;
using System.Threading.Tasks;

namespace RH.Domain.Repositories
{
    public interface IOpportunityCandidateRepository : IRepository<OpportunityCandidate>
    {
        Task<bool> ExistSameCandidateAndTech(int candidateId, int opportunityId);
    }
}
