using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Persistence.Configurations;

public class SocialPostConfiguration : IEntityTypeConfiguration<SocialPost>
{
    public void Configure(EntityTypeBuilder<SocialPost> builder)
    {
        builder.ToTable("SocialPosts");
        builder.HasKey(p => p.Id);
        builder.HasOne<Boutique>()
            .WithMany(b => b.SocialPosts)
            .HasForeignKey(p => p.BoutiqueId);
        builder.Property(p => p.Username)
            .IsRequired();
        builder.Property(p => p.Comment)
            .IsRequired();
        builder.Property(p => p.Timestamp)
            .IsRequired();
    }
}
