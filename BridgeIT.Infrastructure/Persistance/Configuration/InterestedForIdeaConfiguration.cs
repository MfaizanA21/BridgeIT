using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class InterestedForIdeaConfiguration : IEntityTypeConfiguration<InterestedForIdea>
    {
        public void Configure(EntityTypeBuilder<InterestedForIdea> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Interest__3213E83FF623C2C6");

            entity.ToTable("InterestedForIdea");

            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.MeetPlace).HasColumnName("meet_place");
            entity.Property(e => e.MeetTime).HasColumnName("MeetTime");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.IdeaId).HasColumnName("idea_id");

            entity.HasOne(e => e.Student)
                .WithMany(student => student.InterestedForIdeas)
                .HasForeignKey(e => e.StudentId)
                .HasConstraintName("FK__InterestedForIdea_Student");
            // .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Idea)
                .WithMany(idea => idea.InterestedForIdeas)
                .HasForeignKey(e => e.IdeaId)
                .HasConstraintName("FK__InterestedForIdea_Idea");
            // .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
