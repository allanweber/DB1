using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Domain.Entities;

namespace RH.Infrastructure.Repositories.Mappers
{
    public class TechnologyMap : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.ToTable(nameof(Technology));
        }
    }
}
