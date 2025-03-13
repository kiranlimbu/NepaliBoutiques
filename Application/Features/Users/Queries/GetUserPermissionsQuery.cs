using Application.Abstractions;

namespace Application.Features.Users.Queries;

public sealed record GetUserPermissionsQuery(int UserId) : IQuery<List<string>>;

