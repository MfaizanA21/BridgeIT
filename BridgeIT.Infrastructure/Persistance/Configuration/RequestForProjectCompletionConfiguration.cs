using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class RequestForProjectCompletionConfiguration : IEntityTypeConfiguration<RequestForProjectCompletion>
    {
        public void Configure(EntityTypeBuilder<RequestForProjectCompletion> entity)
        {
            entity.HasKey(e => e.id).HasName("PK__RequestT__3213E83F1896BB7E");

            entity.ToTable("RequestToCompletePRoject");

            entity.Property(e => e.id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.RequestStatus)
                .HasColumnName("request_status")
                .HasColumnType("NVARCHAR(25)");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");

            entity.HasOne(d => d.project).WithMany(p => p.requestForProjectCompletions)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__RequestTo__proje__4F47C5E3");
        }
    }
}
