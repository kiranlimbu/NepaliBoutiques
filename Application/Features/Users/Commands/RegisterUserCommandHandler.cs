using Application.Abstractions;
using Application.Abstractions.Authentication;
using Core.Entities;
using Core.Abstractions;
using Core.Abstractions.Repositories;
using Core.Errors;
using Core.ValueObjects;
namespace Application.Features.Users.Commands;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, int>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IAuthenticationService authenticationService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the registration of a new user.
    /// </summary>
    /// <param name="request">The command containing user registration details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A result indicating success or failure of the registration process.</returns>
    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if a user with the provided email already exists in the repository
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is not null)
        {
            // Return a failure result if the user already exists
            return Result.Failure<int>(UserErrors.EmailAlreadyTaken);
        }

        // Create a new user entity with the provided registration details
        var newUser = User.Create(
            0, // id is added by the database 
            request.FirstName, 
            request.LastName, 
            Email.Create(request.Email));

        // Register the user with the authentication service to obtain an identity ID
        var identityId = await _authenticationService.RegisterAsync(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password, 
            request.ConfirmPassword, 
            cancellationToken);

        // TODO: Handle the identityId failure case
        // if (identityId.IsFailure)
        // {
        //     return Result.Failure(identityId.Error);
        // }

        // Set the identity ID for the newly created user
        newUser.SetIdentityId(identityId);

        // Add the new user to the repository
        _userRepository.Add(newUser);

        // Persist changes to the database
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Return a success result with the new user's ID
        return Result.Success(newUser.Id);
    }   
}
