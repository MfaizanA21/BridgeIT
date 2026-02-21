
using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class EducationalResourceConfiguration : IEntityTypeConfiguration<EductionalResource>
    {
        public void Configure(EntityTypeBuilder<EductionalResource> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Educatio__3213E83FC3E922D5");

            entity.ToTable("EducationalResource");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.Title)
                .HasColumnType("nvarchar(max)")
                .IsRequired()
                .HasColumnName("title");

            entity.Property(e => e.Content)
                .HasColumnType("nvarchar(max)")
                .IsRequired()
                .HasColumnName("content");

            entity.Property(e => e.SourceLink)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("source_link");

            entity.Property(e => e.FacultyId)
                .HasColumnName("faculty_id");

            entity.HasOne(d => d.Faculty)
                .WithMany(p => p.EducationalResources)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__Education__facul__756D6ECB");
        }
    }
}
