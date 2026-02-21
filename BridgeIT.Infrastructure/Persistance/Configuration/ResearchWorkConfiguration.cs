using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class ResearchWorkConfiguration : IEntityTypeConfiguration<ResearchWork>
    {
        public void Configure(EntityTypeBuilder<ResearchWork> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Research__3213E83F4DE5E04E");

            entity.ToTable("ResearchWork");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.OtherResearchers)
                .HasMaxLength(255)
                .HasColumnName("other_researchers");
            entity.Property(e => e.PaperName)
                .HasMaxLength(255)
                .HasColumnName("paperName");
            entity.Property(e => e.PublishChannel)
                .HasMaxLength(255)
                .HasColumnName("publish_channel");
            entity.Property(e => e.Link)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("link");
            entity.Property(e => e.YearOfPublish).HasColumnName("year_of_publish");

            entity.HasOne(d => d.Faculty).WithMany(p => p.ResearchWorks)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__ResearchW__facul__1BC821DD");
        }
    }
}
