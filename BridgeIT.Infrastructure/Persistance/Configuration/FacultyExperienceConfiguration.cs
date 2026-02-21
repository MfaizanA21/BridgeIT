
using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class FacultyExperienceConfiguration : IEntityTypeConfiguration<FacultyExperience>
    {
        public void Configure(EntityTypeBuilder<FacultyExperience> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__FacultyE__3213E83F6FF1BC07");

            entity.ToTable("FacultyExperience");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Duration)
                .HasMaxLength(255)
                .HasColumnName("duration");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .HasColumnName("Job_title");
            entity.Property(e => e.Responsibilities).HasMaxLength(255);

            entity.HasOne(d => d.Company).WithMany(p => p.FacultyExperiences)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__FacultyEx__compa__1AD3FDA4");

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultyExperiences)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__FacultyEx__facul__19DFD96B");
        }
    }
}
