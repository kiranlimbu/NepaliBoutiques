using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;
using Core.ValueObjects;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.Email)
            .HasMaxLength(150)
            .IsRequired()
            .HasConversion(
                email => email.Value,
                value => Email.Create(value));
        builder.HasIndex(u => u.Email)
            .IsUnique();
            
            
    }
}

