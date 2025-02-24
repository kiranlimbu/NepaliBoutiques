namespace Application.Features.Users.Responses;

public sealed record AccessTokenResponse(string Token, DateTime Expiration);
