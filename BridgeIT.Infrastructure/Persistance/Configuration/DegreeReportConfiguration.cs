using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class DegreeReportConfiguration : IEntityTypeConfiguration<DegreeReport>
    {
        public void Configure(EntityTypeBuilder<DegreeReport> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__DegreeRe__3213E83FC391FF5B");

            entity.ToTable("DegreeReport");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Achievements).HasMaxLength(255);
            entity.Property(e => e.Activities).HasMaxLength(255);
            entity.Property(e => e.Program)
                .HasMaxLength(255)
                .HasColumnName("program");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Student).WithMany(p => p.DegreeReports)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__DegreeRep__stude__18EBB532");
        }
    }
}
