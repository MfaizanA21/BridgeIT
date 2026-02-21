using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class ProjectProposalConfiguration : IEntityTypeConfiguration<ProjectProposal>
    {
        public void Configure(EntityTypeBuilder<ProjectProposal> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectProposal");

            entity.ToTable("ProjectProposal");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Proposal)
                .HasColumnName("proposal")
                .HasColumnType("varbinary(max)");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.PaymentIntentId).HasColumnName("payment_intent_id")
                .HasMaxLength(30);

            entity.HasOne(d => d.Project).WithMany(p => p.Proposals)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectProposal_Project");

            entity.HasOne(d => d.Student).WithMany(p => p.Proposals)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__ProjectProposal_Student");
        }
    }
}
