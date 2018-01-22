using Microsoft.EntityFrameworkCore;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            return this.dbSet.AsQueryable()
                .Include(c => c.Candidate).Include(c => c.Technology)
                .ToListAsync();
        }
    }
}
