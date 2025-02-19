using Core.Abstractions;
using Core.Errors;
using Core.Events;


namespace Core.Entities;
public sealed class User : Entity
{
    private readonly List<Role> _roles = [];

    private User(int id, string username, string firstName, string lastName, string email)
    {
        Id = id;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string Email { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;

    //TODO: Add these properties
    //public bool IsVerified { get; set; }
    //public required string AccessToken { get; set; }
    //public DateTime TokenExpiryDate { get; set; }

    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

    public static User Create(int id, string username, string firstName, string lastName, string email)
    {
        // validations
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException(UserErrors.UsernameNull.Code, nameof(username));
        }

        if (string.IsNullOrEmpty(firstName))
        {
            throw new ArgumentException(UserErrors.FirstNameNull.Code, nameof(firstName));
        }

        if (string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentException(UserErrors.LastNameNull.Code, nameof(lastName));
        }

        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException(UserErrors.EmailNull.Code, nameof(email));
        }

        //TODO: Add email validation
        // if (!email.IsValidEmail())
        // {
        //     throw new ArgumentException(UserErrors.InvalidEmail.Code, nameof(email));
        // }
        

        var newUser = new User(id, username, firstName, lastName, email);
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
