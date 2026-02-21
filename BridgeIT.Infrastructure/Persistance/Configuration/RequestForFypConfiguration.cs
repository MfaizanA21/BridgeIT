using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class RequestForFypConfiguration : IEntityTypeConfiguration<RequestForFyp>
    {
        public void Configure(EntityTypeBuilder<RequestForFyp> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestF__3213E83F08D70405");
            entity.ToTable("RequestForFyp");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(s => s.StudentId).HasColumnName("student_id");
            entity.Property(f => f.FypId).HasColumnName("fyp_id");

            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasColumnType("int")
                .HasMaxLength(255);

            entity.Property(e => e.IndustryExpertId).HasColumnName("ind_exp_id");

            entity.HasOne(e => e.IndustryExpert)
                .WithMany(p => p.RequestForFyps)
                .HasForeignKey(e => e.IndustryExpertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestFo__ind_e__0697FACD");

            entity.HasOne(e => e.Student)
                .WithMany(p => p.RequestForFyps)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requested_Student");

            entity.HasOne(e => e.Fyp)
                .WithMany(p => p.RequestForFyps)
                .HasForeignKey(d => d.FypId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FOR_FYP");
        }
    }
}
