using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3213E83FA8A3DFA8");

            entity.ToTable("Student");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.skills)
                .HasColumnName("Skills")
                .HasColumnType("nvarchar(max)");
            entity.Property(e => e.department)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("department");
            entity.Property(e => e.RollNumber).HasColumnName("rollNumber");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(f => f.FypId).HasColumnName("fyp_id");
            entity.Property(e => e.cvLink).HasColumnName("cv_link").HasColumnType("NVARCHAR(255)");
            entity.Property(e => e.StripeConnectId)
                .HasColumnName("stripe_connect_id")
                .HasMaxLength(25);

            entity.HasOne(d => d.University).WithMany(p => p.Students)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("FK__Student__univers__1332DBDC");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Student__user_id__14270015");

            entity.HasOne(f => f.Fyp).WithMany(s => s.Students)
                .HasForeignKey(f => f.FypId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student_FYP");
        }
    }
}
