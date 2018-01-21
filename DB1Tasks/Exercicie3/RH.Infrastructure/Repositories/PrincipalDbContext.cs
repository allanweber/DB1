using Microsoft.EntityFrameworkCore;
using RH.Domain.Entities;
using RH.Infrastructure.Repositories.Mappers;

namespace RH.Infrastructure.Repositories
{
    public class PrincipalDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PrincipalDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Technology>(new TechnologyMap());
            modelBuilder.ApplyConfiguration<Opportunity>(new OpportunityMap());
            modelBuilder.ApplyConfiguration<Candidate>(new CandidateMap());
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
