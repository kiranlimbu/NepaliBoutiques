using System.Text.RegularExpressions;

namespace Core.ValueObjects;

/// <summary>
/// Represents a validated email address value object
/// </summary>
public record Email
{
    private readonly string _value;

    /// <summary>
    /// Gets the string representation of the email address
    /// </summary>
    public string Value => _value;

    private Email(string value)
    {
        _value = value;
    }

    /// <summary>
    /// Creates a new EmailAddress instance after validating the input
    /// </summary>
    /// <param name="email">The email address string to validate</param>
    /// <returns>A new EmailAddress instance</returns>
    /// <exception cref="ArgumentException">Thrown when the email format is invalid</exception>
    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email address cannot be empty", nameof(email));

        email = email.Trim();

        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format", nameof(email));

        return new Email(email);
    }

    private static bool IsValidEmail(string email)
    {
        // Using a simplified regex pattern for email validation
        // You might want to use a more comprehensive pattern based on your requirements
        var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;
} 