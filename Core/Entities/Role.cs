using Core.Abstractions;
using Core.Errors;
using Core.Events;

namespace Core.Entities;

public sealed class Role
{
    // predefined roles
    public static readonly Role Admin = new(1, "Admin");
    public static readonly Role RegisteredUser = new(2, "Registered User");
    public static readonly Role BoutiqueOwner = new(3, "Boutique Owner");
    public static readonly Role Customer = new(4, "Tester");

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }

    public ICollection<User> Users { get; private set; } = [];
    public ICollection<Permission> Permissions { get; private set; } = [];
}