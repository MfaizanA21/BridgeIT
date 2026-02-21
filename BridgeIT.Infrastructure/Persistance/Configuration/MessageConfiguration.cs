using BridgeIT.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BridgeIT.Infrastructure.Persistance.Configuration
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> entity)
        {
            entity.HasKey(m => m.Id).HasName("PK__Message__3213E83F25D4FD14");

            entity.ToTable("Message");

            entity.Property(e => e.SenderId).HasColumnName("sender_id").HasColumnType("uniqueidentifier");
            entity.Property(e => e.RecipientId).HasColumnName("recipient_id").HasColumnType("uniqueidentifier");
            entity.Property(e => e.Content).HasColumnName("content").HasColumnType("NVARCHAR(MAX)");
            entity.Property(e => e.TimeSent).HasColumnName("time_stamp");
        }
    }
}
