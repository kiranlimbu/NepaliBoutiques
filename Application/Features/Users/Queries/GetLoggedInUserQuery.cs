using Application.Abstractions;
using Application.Features.Users.Responses;

namespace Application.Features.Users.Queries;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;


