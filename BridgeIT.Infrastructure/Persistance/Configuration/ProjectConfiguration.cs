using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Project__3213E83F9E55023D");

            entity.ToTable("Project");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(255)
                .HasColumnName("current_status");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IndExpertId).HasColumnName("ind_expert_id");
            entity.Property(e => e.Stack)
                .HasMaxLength(255)
                .HasColumnName("stack");
            entity.Property(e => e.Link).HasColumnName("link").HasMaxLength(255);
            entity.Property(e => e.Budget).HasColumnName("budget");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.Team).HasColumnName("team");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.IndExpert).WithMany(p => p.Projects)
                .HasForeignKey(d => d.IndExpertId)
                .HasConstraintName("FK__Project__ind_exp__1F98B2C1");

            entity.HasOne(d => d.Student).WithMany(p => p.Projects)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Project__student__1EA48E88");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Projects)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_Project_Faculty");
        }
    }
}
