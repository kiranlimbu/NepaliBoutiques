using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Persistence.Configurations;

public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("InventoryItems");
        builder.HasKey(i => i.Id);
        builder.HasOne<Boutique>()
            .WithMany(b => b.Inventories)
            .HasForeignKey(i => i.BoutiqueId);
        builder.Property(i => i.ImageUrl)
            .IsRequired();
    }
}
