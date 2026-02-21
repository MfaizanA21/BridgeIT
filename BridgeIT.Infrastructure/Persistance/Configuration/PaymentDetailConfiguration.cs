using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class PaymentDetailConfiguration : IEntityTypeConfiguration<PaymentDetail>
    {
        public void Configure(EntityTypeBuilder<PaymentDetail> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentD__3213E83F79822DA9");

            entity.ToTable("PaymentDetail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.ProjectId)
                .HasColumnName("project_id");

            entity.Property(e => e.PaidAt)
                .HasColumnName("paid_at");

            entity.Property(e => e.PaymentSlip)
                .IsRequired()
                .HasColumnName("payment_slip");

            entity.HasIndex(e => e.ProjectId)
                .IsUnique()
                .HasDatabaseName("UQ_PaymentDetail_ProjectId");

            entity.HasOne(e => e.Project)
                .WithOne(p => p.PaymentDetail)
                .HasForeignKey<PaymentDetail>(e => e.ProjectId)
                .HasConstraintName("FK__PaymentDe__proje__625A9A57");
        }
    }
}
