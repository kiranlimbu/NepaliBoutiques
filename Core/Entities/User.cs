using Core.Abstractions;
using Core.Events;
using Core.ValueObjects;

namespace Core.Entities;
public sealed class User : BaseEntity
{
    private readonly List<Role> _roles = [];

    private User(int id, string firstName, string lastName, Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public Email Email { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;

    //TODO: Add these properties
    //public bool IsVerified { get; set; }
    //public required string AccessToken { get; set; }
    //public DateTime TokenExpiryDate { get; set; }

    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

    public static User Create(int id, string firstName, string lastName, Email email)
    {
        var newUser = new User(id, firstName, lastName, email);
        // raise events
        newUser.RaiseCoreEvent(new UserCreatedCoreEvent(newUser));

        newUser._roles.Add(Role.RegisteredUser);
        // return the new user
        return newUser;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }

    public void AddRole(Role role)
    {
        _roles.Add(role);
    }

    public void RemoveRole(Role role)
    {
        _roles.Remove(role);
    }

}
