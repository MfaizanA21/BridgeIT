using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class ProjectProgressConfiguration : IEntityTypeConfiguration<ProjectProgress>
    {
        public void Configure(EntityTypeBuilder<ProjectProgress> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectP__3213E83FDFBC58FE");

            entity.ToTable("ProjectProgress");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Task)
                .HasColumnName("task")
                .HasColumnType("NVARCHAR(MAX)");
            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("NVARCHAR(MAX)");
            entity.Property(e => e.TaskStatus)
                .HasColumnName("task_status")
                .HasColumnType("VARCHAR(25)");

            entity.HasOne(p => p.Project).WithMany(pp => pp.ProjectProgresses)
                .HasForeignKey(p => p.ProjectId)
                .HasConstraintName("FK__ProjectPr__proje__3C34F16F");
        }
    }
}
