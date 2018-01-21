using RH.Domain.Entities;
using RH.Domain.Repositories;

namespace RH.Infrastructure.Repositories
{
    public class TechnologyRepository: Repository<Technology>, ITechnologyRepository
    {
        public TechnologyRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
