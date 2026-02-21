using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class BoughtFypConfiguration : IEntityTypeConfiguration<BoughtFyp>
    {
        public void Configure(EntityTypeBuilder<BoughtFyp> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__BoughtFY__3213E83FCEA1BCAA");

            entity.ToTable("BoughtFYP");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Agreement)
                .HasColumnType("varbinary(max)")
                .HasColumnName("agreement");
            entity.Property(e => e.FypId).HasColumnName("fyp_id");
            entity.Property(e => e.IndExpertId).HasColumnName("ind_expert_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.UniversityAdminId).HasColumnName("university_admin_id");

            entity.HasOne(d => d.Fyp).WithMany(p => p.BoughtFyps)
                .HasForeignKey(d => d.FypId)
                .HasConstraintName("FK__BoughtFYP__fyp_i__25518C17");

            entity.HasOne(d => d.IndExpert).WithMany(p => p.BoughtFyps)
                .HasForeignKey(d => d.IndExpertId)
                .HasConstraintName("FK__BoughtFYP__ind_e__2645B050");

            entity.HasOne(d => d.UniversityAdmin).WithMany(p => p.BoughtFyps)
                .HasForeignKey(d => d.UniversityAdminId)
                .HasConstraintName("FK__BoughtFYP__unive__2739D489");
        }
    }
}
