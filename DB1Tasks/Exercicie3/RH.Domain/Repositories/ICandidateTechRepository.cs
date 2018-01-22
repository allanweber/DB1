using RH.Domain.Core.Repositories;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Domain.Repositories
{
    public interface ICandidateTechRepository: IRepository<CandidateTech>
    {
        Task<bool> ExistSameCandidateAndTech(int candidateId, int techId);

        Task<List<CandidateTech>> GetByTechnology(int techId);

        Task<List<CandidateTechnologyDto>> GetAllTechnologyByCandidate(int candidateId);
    }
}
