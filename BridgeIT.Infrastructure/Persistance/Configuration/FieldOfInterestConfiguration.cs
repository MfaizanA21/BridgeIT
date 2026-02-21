
using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class FieldOfInterestConfiguration : IEntityTypeConfiguration<FieldOfInterest>
    {
        public void Configure(EntityTypeBuilder<FieldOfInterest> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__FieldOfI__3213E83F670D4F91");

            entity.ToTable("FieldOfInterest");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FieldOfInterest1)
                .HasMaxLength(255)
                .HasColumnName("field_of_interest");
        }
    }
}
