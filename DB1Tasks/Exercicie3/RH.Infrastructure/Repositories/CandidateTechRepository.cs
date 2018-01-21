using RH.Domain.Entities;
using RH.Domain.Repositories;

namespace RH.Infrastructure.Repositories
{
    public class CandidateTechRepository: Repository<CandidateTech>, ICandidateTechRepository
    {
        public CandidateTechRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
