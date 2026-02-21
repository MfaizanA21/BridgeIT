using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3213E83F73A8ACC8");

            entity.ToTable("Event");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EventDate)
                .HasColumnType("datetime")
                .HasColumnName("event_date");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.SpeakerName)
                .HasMaxLength(255)
                .HasColumnName("speaker_name");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Venue)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("venue");
            entity.Property(e => e.Description)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("description");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Events)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__Event__faculty_i__1CBC4616");
        }
    }
}
