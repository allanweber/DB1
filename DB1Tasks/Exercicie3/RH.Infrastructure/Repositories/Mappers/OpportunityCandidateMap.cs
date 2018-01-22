using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Domain.Entities;

namespace RH.Infrastructure.Repositories.Mappers
{
    public class OpportunityCandidateMap: IEntityTypeConfiguration<OpportunityCandidate>
    {
        public void Configure(EntityTypeBuilder<OpportunityCandidate> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.ToTable(nameof(OpportunityCandidate));
        }
    }
}
