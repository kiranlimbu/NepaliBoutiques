using Core.Abstractions;

namespace Core.Errors;

public static class UserErrors
{
    public static Error UsernameNull { get; } = new Error("Username_Null", "Username cannot be null");
    public static Error FirstNameNull { get; } = new Error("FirstName_Null", "First name cannot be null");
    public static Error LastNameNull { get; } = new Error("LastName_Null", "Last name cannot be null");
    public static Error EmailNull { get; } = new Error("Email_Null", "Email cannot be null");
    public static Error InvalidEmail { get; } = new Error("Invalid_Email", "Email provided is invalid");
    public static Error UsernameAlreadyTaken { get; } = new Error("Username_Already_Taken", "Username is already taken");
    public static Error EmailAlreadyTaken { get; } = new Error("Email_Already_Taken", "Email is already taken");
}
