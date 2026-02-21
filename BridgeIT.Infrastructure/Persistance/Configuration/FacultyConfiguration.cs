
using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Faculty__3213E83F3ED2B942");

            entity.ToTable("Faculty");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Interest)
                .HasMaxLength(255)
                .HasColumnName("interest");
            entity.Property(e => e.Post)
                .HasMaxLength(255)
                .HasColumnName("post");
            entity.Property(e => e.Department)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("department");
            entity.Property(e => e.UniId).HasColumnName("uni_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Uni).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__Faculty__uni_id__160F4887");

            entity.HasOne(d => d.User).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Faculty__user_id__151B244E");
        }
    }
}
