using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser
{
    // This will be the foreign key to your domain User entity
    public int UserId { get; set; }
}
