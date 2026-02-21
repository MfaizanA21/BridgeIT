
using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class FypConfiguration : IEntityTypeConfiguration<Fyp>
    {
        public void Configure(EntityTypeBuilder<Fyp> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__FYP__3213E83F8A77B011");

            entity.ToTable("FYP");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Batch)
                .HasMaxLength(255)
                .HasColumnName("batch");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.Members).HasColumnName("members");
            entity.Property(i => i.fyp_id)
                .HasMaxLength(25)
                .HasColumnName("fyp_id");
            entity.Property(s => s.Status)
                .HasMaxLength(25)
                .HasColumnName("status");
            entity.Property(e => e.Technology)
                .HasMaxLength(255)
                .HasColumnName("technology");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(f => f.YearOfCompletion)
                .HasColumnName("year_of_completion")
                .HasColumnType("int");
            entity.HasOne(d => d.Faculty).WithMany(p => p.Fyps)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__FYP__faculty_id__65F62111");
        }
    }
}
