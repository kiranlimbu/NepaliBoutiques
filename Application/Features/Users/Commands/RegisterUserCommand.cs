using Application.Abstractions;

namespace Application.Features.Users.Commands;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : ICommand<int>;
