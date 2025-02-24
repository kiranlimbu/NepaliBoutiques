using Application.Abstractions;
using Application.Features.Users.Responses;


namespace Application.Features.Users.Commands;

public sealed record LogInUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;


