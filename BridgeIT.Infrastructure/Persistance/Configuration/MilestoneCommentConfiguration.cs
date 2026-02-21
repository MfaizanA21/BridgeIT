
using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class MilestoneCommentConfiguration : IEntityTypeConfiguration<MilestoneComment>
    {
        public void Configure(EntityTypeBuilder<MilestoneComment> entity)
        {
            entity.HasKey((e => e.Id)).HasName("PK__Mileston__3213E83F7B39521F");

            entity.ToTable("MilestoneComment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasColumnName("comment")
                .HasColumnType("NVARCHAR(MAX)");
            entity.Property(e => e.CommentDate)
                .HasColumnType("datetime")
                .HasColumnName("comment_date");

            entity.Property(e => e.Commenter_id).HasColumnName("commenter_id");
            entity.Property(e => e.Milestone_id).HasColumnName("milestone_id");

            entity.HasOne(o => o.MileStone).WithMany(m => m.MilestoneComments)
                .HasForeignKey(o => o.Milestone_id)
                .HasConstraintName("FK__Milestone__miles_2F9A1060");
            entity.HasOne(i => i.Commenter).WithMany(i => i.MilestoneComments)
                .HasForeignKey(i => i.Commenter_id)
                .HasConstraintName("FK__Milestone__comme__2EA5EC27");
        }
    }
}
