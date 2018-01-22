using RH.Domain.Core.Repositories;
using RH.Domain.Entities;
using System.Threading.Tasks;

namespace RH.Domain.Repositories
{
    public interface ICandidateTechRepository: IRepository<CandidateTech>
    {
        Task<bool> ExistSameCandidateAndTech(int candidateId, int techId);
    }
}
