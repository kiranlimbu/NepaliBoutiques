using Application.Abstractions.Authentication;
using Core.Entities;
using Core.ValueObjects;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.AuthenticationService;


/// <summary>
/// Service responsible for handling user authentication operations including registration and login.
/// Implements the IAuthenticationService interface.
/// </summary>
internal sealed class AuthenticationService : IAuthenticationService
{
    // UserManager is used to create and manage users in the Microsoft Identity system
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    /// <summary>
    /// Initializes a new instance of the AuthenticationService class.
    /// </summary>
    /// <param name="userManager">The ASP.NET Core Identity UserManager for managing users</param>
    /// <param name="jwtService">Service for generating JWT tokens</param>
    public AuthenticationService(UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    /// <summary>
    /// Registers a new user in the system and generates their authentication token.
    /// </summary>
    /// <param name="firstName">The user's first name</param>
    /// <param name="lastName">The user's last name</param>
    /// <param name="email">The user's email address which will also serve as their username</param>
    /// <param name="password">The user's password</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed</param>
    /// <returns>A JWT token for the newly registered user</returns>
    /// <exception cref="Exception">Thrown when user creation fails</exception>
    /// <exception cref="ApplicationException">Thrown when token generation fails</exception>
    public async Task<string> RegisterAsync(string firstName, string lastName, string email, string password, CancellationToken cancellationToken = default)
    {
        var appUser = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(appUser, password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        var identityId = appUser.Id;

        return identityId;
    }

    /// <summary>
    /// Authenticates a user using their email and password.
    /// </summary>
    /// <param name="email">The user's email address</param>
    /// <param name="password">The user's password</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed</param>
    /// <returns>The user's ID if authentication is successful, null if user is not found</returns>
    /// <exception cref="ApplicationException">Thrown when password validation fails</exception>
    public async Task<string?> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return null;
        }

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
        {
            throw new ApplicationException("Invalid email or password");
        }

        var token = await _jwtService.GenerateTokenAsync(email, password, cancellationToken);

        if (token.IsFailure)
        {
            throw new ApplicationException($"Failed to generate token: {token.Error}");
        }

        return token.Value;
    }
}
