using Microsoft.EntityFrameworkCore;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Infrastructure.Repositories
{
    public class OpportunityCandidateRepository : Repository<OpportunityCandidate>, IOpportunityCandidateRepository
    {
        public OpportunityCandidateRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<bool> ExistSameCandidateAndTech(int candidateId, int opportunityId)
        {
            return await this.ExistsAsync(c => c.CandidateId == candidateId && c.OpportunityId == opportunityId);
        }

        public override Task<OpportunityCandidate> GetAsync(params object[] keys)
        {
            return this.Query()
                .Where(c => c.Id == int.Parse(keys[0].ToString()))
                .Include(c => c.Candidate).Include(c => c.Opportunity).FirstOrDefaultAsync();

        }

        public override Task<List<OpportunityCandidate>> GetAllAsync()
        {
            return this.dbSet.AsQueryable().Include(c => c.Candidate).Include(c => c.Opportunity)
                .ToListAsync();
        }
    }
}
