using RH.Domain.Entities;
using RH.Domain.Repositories;

namespace RH.Infrastructure.Repositories
{
    public class OpportunityRepository: Repository<Opportunity>, IOpportunityRepository
    {
        public OpportunityRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
