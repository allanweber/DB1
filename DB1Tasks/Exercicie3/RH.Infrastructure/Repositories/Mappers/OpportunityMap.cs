using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.Infrastructure.Repositories.Mappers
{
    public class  OpportunityMap : IEntityTypeConfiguration<Opportunity>
    {
        public void Configure(EntityTypeBuilder<Opportunity> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.ToTable(nameof(Opportunity));
        }
    }
}
