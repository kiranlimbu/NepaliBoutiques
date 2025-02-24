using Application.Abstractions;
using Application.Abstractions.Authentication;
using Application.Features.Users.Responses;
using Core.Abstractions;
using Core.Abstractions.Repositories;
using Core.Errors;

namespace Application.Features.Users.Commands;

internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public LogInUserCommandHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        // Check if the user exists in the repository
        // var user = await _userRepository.GetByEmailAsync(request.Email);
        // if (user is null)
        // {
        //     // Return a failure result if the user does not exist
        //     return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        // }

        // Check if the provided password matches the user's password
        // if (!user.Password.Equals(request.Password))
        // {
        //     // Return a failure result if the password is incorrect
        //     return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        // }

        // Generate a JWT token for the user    
        var tokenResult = await _jwtService.GenerateTokenAsync(request.Email, request.Password, cancellationToken);

        if (tokenResult.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        // Return the token and expiration date
        return new AccessTokenResponse(tokenResult.Value, DateTime.UtcNow.AddHours(5));
    }
}
