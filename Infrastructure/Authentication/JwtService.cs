using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions.Authentication;
using Core.Abstractions;
using Core.Abstractions.Repositories;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;
    public JwtService(IConfiguration configuration, UserManager<ApplicationUser> userManager, JwtSettings jwtSettings, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userManager = userManager;
        _jwtSettings = jwtSettings;
        _userRepository = userRepository;
    }
    public async Task<Result<string>> GenerateTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new ApplicationException("User not found");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            throw new ApplicationException("Invalid password");
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var permissions = await _userRepository.GetUserPermissionsAsync(user.UserId, cancellationToken);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.NameIdentifier, user.Id), // IdentityId
            new Claim("UserId", user.UserId.ToString()), // Entity UserId
        };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permission", permission));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(_jwtSettings.ExpiryDays);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Result.Success(jwtToken);
    }
}
