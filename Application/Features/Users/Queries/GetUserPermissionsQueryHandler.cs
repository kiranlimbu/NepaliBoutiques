using Application.Abstractions;
using Application.Abstractions.Authentication;
using Core.Abstractions;
using Dapper;

namespace Application.Features.Users.Queries;

public sealed class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, List<string>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;

    public GetUserPermissionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    public async Task<Result<List<string>>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                Name
            FROM Permissions AS P
            INNER JOIN RolePermissions AS RP ON P.Id = RP.PermissionId
            INNER JOIN RoleUser AS RU ON RU.RoleId = R.Id
            WHERE RU.UserId = @UserId
        """;

        var permissions = await connection.QueryAsync<string>(
            sql,
            new { UserId = request.UserId });

        return permissions.ToList();
    }
}
