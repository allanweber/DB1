using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Domain.Entities;

namespace RH.Infrastructure.Repositories.Mappers
{
    public class OpportunityTechMap:IEntityTypeConfiguration<OpportunityTech>
    {
        public void Configure(EntityTypeBuilder<OpportunityTech> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.ToTable(nameof(OpportunityTech));
    }
}
}
