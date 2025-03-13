using Application.Abstractions;
using Application.Abstractions.Authentication;
using Application.Features.Users.Responses;
using Core.Abstractions;
using Core.Errors;
using Dapper;

namespace Application.Features.Users.Queries;

/// <summary>
/// Handles the query to retrieve the currently logged-in user's details.
/// </summary>
internal sealed class GetLoggedInUserQueryHandler : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;
    
    public GetLoggedInUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    /// <summary>
    /// Handles the query to retrieve the logged-in user's details from the database.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A result containing the user response if found, otherwise a failure result.</returns>
    public async Task<Result<UserResponse>> Handle(GetLoggedInUserQuery request, CancellationToken cancellationToken)
    {
        // Create a new database connection
        using var connection = _sqlConnectionFactory.CreateConnection();

        // SQL query to select user details based on the identity ID
        const string sql = """
            SELECT 
                Id, 
                Username, 
                Email,   
                FirstName, 
                LastName
            FROM Users
            WHERE identityId = @IdentityId
        """;    

        // Execute the query with the current user's identity ID and retrieve the user details
        var user = await connection.QueryFirstOrDefaultAsync<UserResponse>(
            sql, 
            new { _userContext.IdentityId });

        // Check if the user was found and return a failure result if not
        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);
        }

        // Return the user details as a successful result
        return user;
    }
}
