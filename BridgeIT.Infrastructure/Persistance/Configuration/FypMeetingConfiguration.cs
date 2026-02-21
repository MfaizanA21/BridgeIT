
using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class FypMeetingConfiguration : IEntityTypeConfiguration<FypMeeting>
    {
        public void Configure(EntityTypeBuilder<FypMeeting> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__FypMeeti__3213E83F1DF49EAE");

            entity.ToTable("FypMeeting");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.ChosenSlot)
                .HasColumnName("chosen_slot");

            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");

            entity.Property(e => e.MeetLink)
                .HasColumnName("meet_link");

            entity.Property(e => e.Feedback)
                .HasColumnName("feedback");

            entity.Property(e => e.IsMeetDone)
                .HasColumnName("is_meet_done");

            entity.Property(e => e.FypId)
                .HasColumnName("fyp_id");

            entity.Property(e => e.IndExpId)
                .HasColumnName("ind_exp_id");

            entity.HasOne(d => d.Fyp)
                .WithMany(p => p.FypMeetings)
                .HasForeignKey(d => d.FypId)
                .HasConstraintName("FK__FypMeetin__fyp_i__1B9317B3");

            entity.HasOne(d => d.IndustryExpert)
                .WithMany(p => p.FypMeetings)
                .HasForeignKey(d => d.IndExpId)
                .HasConstraintName("FK__FypMeetin__ind_e__1C873BEC");
        }
    }
}
