using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class MileStoneConfiguration : IEntityTypeConfiguration<MileStone>
    {
        public void Configure(EntityTypeBuilder<MileStone> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__MileSton__3213E83F3971CE7B");

            entity.ToTable("MileStone");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AchievementDate).HasColumnName("achievement_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Project).WithMany(p => p.MileStones)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__MileStone__proje__208CD6FA");
        }
    }
}
