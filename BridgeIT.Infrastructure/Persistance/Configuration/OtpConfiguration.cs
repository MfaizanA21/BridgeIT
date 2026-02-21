using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class OtpConfiguration : IEntityTypeConfiguration<Otp>
    {
        public void Configure(EntityTypeBuilder<Otp> entity)
        {
            entity.HasKey(e => e.email).HasName("PK__Otp__AB6E616532361977");

            entity.ToTable("Otp");

            entity.Property(e => e.email)
                .HasMaxLength(255)
                .HasColumnName("email");

            entity.Property(e => e.otp)
                .HasColumnName("otp");

            entity.Property(e => e.created_at)
                .HasMaxLength(255)
                .HasColumnName("created_at");
        }
    }
}
