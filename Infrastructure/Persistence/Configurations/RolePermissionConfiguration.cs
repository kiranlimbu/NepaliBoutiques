using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(rp => rp.RoleId);
        builder.HasOne<Permission>()
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);
        builder.HasData(
            new RolePermission 
            { 
                RoleId = Role.RegisteredUser.Id, 
                PermissionId = Permission.ViewUsers.Id 
            }
        );
    }
}
