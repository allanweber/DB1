using Microsoft.EntityFrameworkCore;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
