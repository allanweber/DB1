using Microsoft.EntityFrameworkCore;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<OpportunityTechnologyDto>> GetAllTechnologyByOpportunity(int opportunityId)
        {
            var query = await this.Query()
                .Include(o => o.Opportunity).Include(c => c.Technology)
                .Where(o => o.OpportunityId == opportunityId)
                .OrderByDescending(o => o.Percentage)
                .ToListAsync();

            return (from tech in query
                    select new OpportunityTechnologyDto { Id = tech.Id, Name = tech.Technology.Name, Percentage = tech.Percentage })
                    .ToList();
        }
    }
}
