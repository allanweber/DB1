using RH.Domain.Entities;
using RH.Domain.Repositories;

namespace RH.Infrastructure.Repositories
{
    public class CandidateRepository: Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
