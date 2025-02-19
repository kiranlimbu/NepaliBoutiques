using Application.Abstractions;
using Application.Features.Boutiques.Responses;
using Core.Abstractions;
using Dapper;

namespace Application.Features.Boutiques.Queries;

/// <summary>
/// Handles the query to search for boutiques by name.
/// </summary>
internal sealed class SearchBoutiqueByNameQueryHandler : IQueryHandler<SearchBoutiqueByNameQuery, IReadOnlyList<BoutiqueResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchBoutiqueByNameQueryHandler"/> class.
    /// </summary>
    /// <param name="sqlConnectionFactory">The SQL connection factory to create database connections.</param>
    public SearchBoutiqueByNameQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    /// <summary>
    /// Handles the query to search boutiques by name.
    /// </summary>
    /// <param name="request">The query request containing the boutique name to search for.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A result containing a list of boutique responses matching the search criteria.</returns>
    public async Task<Result<IReadOnlyList<BoutiqueResponse>>> Handle(SearchBoutiqueByNameQuery request, CancellationToken cancellationToken)
    {
        // Check if the name is null or empty and return an empty list if true
        if (string.IsNullOrEmpty(request.Name))
        {
            return new List<BoutiqueResponse>();
        }

        // Create a new database connection
        using var connection = _sqlConnectionFactory.CreateConnection();

        // SQL query to select boutiques where the name matches the search pattern
        const string sql = """
            SELECT 
                Id,
                Name,
                ProfilePicture,
                Followers,
                Description,
                Contact,
                InstagramLink
            FROM Boutiques
            WHERE Name LIKE @Name
        """;

        // Execute the query with the provided name parameter and retrieve matching boutiques
        var boutiques = await connection.QueryAsync<BoutiqueResponse>(sql, new { Name = $"%{request.Name}%" });

        // Return the result as a list
        return boutiques.ToList();
    }
}

