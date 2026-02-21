using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class IdeaConfiguration : IEntityTypeConfiguration<Idea>
    {
        public void Configure(EntityTypeBuilder<Idea> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Idea__3213E83FFDEF3EB1");

            entity.ToTable("Idea");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Technology)
                .HasMaxLength(255)
                .HasColumnName("technology");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Ideas)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__Idea__faculty_id__1DB06A4F");
        }
    }
}
