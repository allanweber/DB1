using Microsoft.EntityFrameworkCore;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Infrastructure.Repositories
{
    public class CandidateTechRepository: Repository<CandidateTech>, ICandidateTechRepository
    {
        public CandidateTechRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<bool> ExistSameCandidateAndTech(int candidateId, int techId)
        {
            return await this.ExistsAsync(c => c.CandidateId == candidateId && c.TechnologyId == techId);
        }

        public override Task<CandidateTech> GetAsync(params object[] keys)
        {
            return this.Query()
                .Where(c => c.Id == int.Parse(keys[0].ToString()))
                .Include(c => c.Candidate).Include(c => c.Technology).FirstOrDefaultAsync();
                
        }

        public override Task<List<CandidateTech>> GetAllAsync()
        {
            return this.Query()
                .Include(c => c.Candidate).Include(c => c.Technology)
                .ToListAsync();
        }

        public Task<List<CandidateTech>> GetByTechnology(int techId)
        {
            return this.Query()
                .Include(c => c.Candidate).Include(c => c.Technology)
                .Where(c => c.TechnologyId == techId)
                .OrderByDescending(c=> c.Percentage)
                .ToListAsync();
        }

        public async Task<List<CandidateTechnologyDto>> GetAllTechnologyByCandidate(int candidateId)
        {
            var query = await this.Query()
                .Include(c => c.Candidate).Include(c => c.Technology)
                .Where(c => c.CandidateId == candidateId)
                .ToListAsync();

            return (from tech in query
                    select new CandidateTechnologyDto { Id = tech.Id, Name = tech.Technology.Name, Percentage = tech.Percentage })
                    .ToList();
        }
    }
}
