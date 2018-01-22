using Microsoft.EntityFrameworkCore;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Infrastructure.Repositories
{
    public class OpportunityTechRepository: Repository<OpportunityTech>, IOpportunityTechRepository
    {
        public OpportunityTechRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<bool> ExistSameCandidateAndTech(int opportunityId, int techId)
        {
            return await this.ExistsAsync(c => c.OpportunityId == opportunityId && c.TechnologyId == techId);
        }

        public override Task<OpportunityTech> GetAsync(params object[] keys)
        {
            return this.Query()
                .Where(c => c.Id == int.Parse(keys[0].ToString()))
                .Include(c => c.Opportunity).Include(c => c.Technology).FirstOrDefaultAsync();

        }

        public override Task<List<OpportunityTech>> GetAllAsync()
        {
            return this.dbSet.AsQueryable()
                .Include(c => c.Opportunity).Include(c => c.Technology)
                .ToListAsync();
        }
    }
}
