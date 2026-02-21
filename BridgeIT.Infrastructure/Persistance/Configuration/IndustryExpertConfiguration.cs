
using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class IndustryExpertConfiguration : IEntityTypeConfiguration<IndustryExpert>
    {
        public void Configure(EntityTypeBuilder<IndustryExpert> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Industry__3213E83FA9545AA2");

            entity.ToTable("IndustryExpert");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Contact)
                .HasMaxLength(255)
                .HasColumnName("contact");
            entity.Property(e => e.Post)
                .HasMaxLength(255)
                .HasColumnName("post");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Company).WithMany(p => p.IndustryExperts)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__IndustryE__compa__17F790F9");

            entity.HasOne(d => d.User).WithMany(p => p.IndustryExperts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__IndustryE__user___17036CC0");
        }
    }
}
