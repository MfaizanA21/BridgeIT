using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class SponsoredFypConfiguration : IEntityTypeConfiguration<SponsoredFyp>
    {
        public void Configure(EntityTypeBuilder<SponsoredFyp> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Sponsore__3213E83F08D49FDF");

            entity.ToTable("SponsoredFYP");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Agreement)
                .HasColumnType("varbinary(max)")
                .HasColumnName("agreement");
            entity.Property(e => e.FypId).HasColumnName("fyp_id");
            entity.Property(e => e.SponsoredById).HasColumnName("sponsored_by_id");

            entity.HasOne(d => d.Fyp).WithMany(p => p.SponsoredFyps)
                .HasForeignKey(d => d.FypId)
                .HasConstraintName("FK__Sponsored__fyp_i__282DF8C2");

            entity.HasOne(d => d.SponsoredBy).WithMany(p => p.SponsoredFyps)
                .HasForeignKey(d => d.SponsoredById)
                .HasConstraintName("FK__Sponsored__spons__29221CFB");
        }
    }
}
