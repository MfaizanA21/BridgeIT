using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class ProjectModuleConfiguration : IEntityTypeConfiguration<ProjectModule>
    {
        public void Configure(EntityTypeBuilder<ProjectModule> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectM__3213E83F17D82272");

            entity.ToTable("ProjectModule");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description");

            entity.Property(e => e.Status)
                .HasColumnType("BIT")
                .HasColumnName("status");

            entity.Property(e => e.ProjectId)
                .HasColumnName("project_id");

            entity.HasOne(d => d.Project)
                .WithMany(p => p.Modules)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectMo__proje__2EA5EC27");
        }
    }
}
