using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Domain.Entities;

namespace RH.Infrastructure.Repositories.Mappers
{
    public class CandidateMap : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.ToTable(nameof(Candidate));
        }
    }
}
