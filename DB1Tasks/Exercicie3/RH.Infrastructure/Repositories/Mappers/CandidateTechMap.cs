using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Domain.Entities;

namespace RH.Infrastructure.Repositories.Mappers
{
    public class CandidateTechMap : IEntityTypeConfiguration<CandidateTech>
    {
        public void Configure(EntityTypeBuilder<CandidateTech> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.ToTable(nameof(CandidateTech));
        }
    }
}
