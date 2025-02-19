using Application.Abstractions;

namespace Application.Features.Users.Commands;

public sealed record RegisterUserCommand(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword
) : ICommand<int>;
