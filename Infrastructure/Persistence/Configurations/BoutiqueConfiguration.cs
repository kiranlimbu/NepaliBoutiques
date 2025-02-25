using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Persistence.Configurations;

public class BoutiqueConfiguration : IEntityTypeConfiguration<Boutique>
{
    public void Configure(EntityTypeBuilder<Boutique> builder)
    {
        builder.ToTable("Boutiques");
        builder.HasKey(b => b.Id);
        builder.HasMany(b => b.Inventories)
            .WithOne()
            .HasForeignKey(i => i.BoutiqueId);
        builder.HasMany(b => b.SocialPosts)
            .WithOne()
            .HasForeignKey(p => p.BoutiqueId);
        builder.Property(b => b.Name)
            .HasMaxLength(150)
            .IsRequired();
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(b => b.OwnerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(b => b.ProfilePicture)
            .IsRequired();
        builder.Property(b => b.Category)
            .HasMaxLength(300)
            .IsRequired();
        builder.Property(b => b.Location)
            .HasMaxLength(150)
            .IsRequired();
        builder.Property(b => b.Contact)
            .HasMaxLength(150)
            .IsRequired();
            
        builder.HasIndex(b => b.Name)
            .IsUnique();
        builder.HasIndex(b => b.Category);
        builder.HasIndex(b => b.Location);
        // Configures the 'Version' property as a concurrency token using SQL Server's row versioning.
        // This helps prevent race conditions during updates by ensuring that the record hasn't been modified
        // by another transaction. The database will automatically update this value on each modification.
        // Useful for implementing optimistic concurrency control.
        builder.Property<uint>("Version").IsRowVersion();
    }
}

