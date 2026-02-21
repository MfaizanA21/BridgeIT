using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class UniversityAdminConfiguration : IEntityTypeConfiguration<UniversityAdmin>
    {
        public void Configure(EntityTypeBuilder<UniversityAdmin> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Universi__3213E83FDD7E6C0B");

            entity.ToTable("UniversityAdmin");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Contact)
                .HasMaxLength(255)
                .HasColumnName("contact");
            entity.Property(e => e.OfficeAddress)
                .HasMaxLength(255)
                .HasColumnName("officeAddress");
            entity.Property(e => e.UniId).HasColumnName("uni_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Uni).WithMany(p => p.UniversityAdmins)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__Universit__uni_i__123EB7A3");

            entity.HasOne(d => d.User).WithMany(p => p.UniversityAdmins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Universit__user___114A936A");
        }
    }
}
