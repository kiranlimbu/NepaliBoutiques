using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(r => r.Id);
        builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();
        // seed data
        builder.HasData(Role.Admin, Role.BoutiqueOwner, Role.Tester, Role.RegisteredUser);
    }
}
