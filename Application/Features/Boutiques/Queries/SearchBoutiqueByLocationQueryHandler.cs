using Application.Abstractions;
using Application.Features.Boutiques.Responses;
using Core.Abstractions;
using Dapper;

namespace Application.Features.Boutiques.Queries;

/// <summary>
/// Handles the query to search for boutiques by location.
/// </summary>
internal sealed class SearchBoutiqueByLocationQueryHandler : IQueryHandler<SearchBoutiqueByLocationQuery, IReadOnlyList<BoutiqueResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchBoutiqueByLocationQueryHandler"/> class.
    /// </summary>
    /// <param name="sqlConnectionFactory">The SQL connection factory to create database connections.</param>
    public SearchBoutiqueByLocationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    /// <summary>
    /// Handles the query to search boutiques by location.
    /// </summary>
    /// <param name="request">The query request containing the location to search for.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A result containing a list of boutique responses matching the search criteria.</returns>
    public async Task<Result<IReadOnlyList<BoutiqueResponse>>> Handle(SearchBoutiqueByLocationQuery request, CancellationToken cancellationToken)
    {
        // Check if the location is null or empty and return an empty list if true
        if (string.IsNullOrEmpty(request.Location))
        {
            return new List<BoutiqueResponse>();
        }

        // Create a new database connection
        using var connection = _sqlConnectionFactory.CreateConnection();

        // SQL query to select boutiques where the location matches the search criteria
        const string sql = """
            SELECT 
                Id,
                Name,
                ProfilePicture,
                InstagramLink
            FROM Boutiques
            WHERE Location = @Location
        """;
        
        // Execute the query with the provided location parameter and retrieve matching boutiques
        var boutiques = await connection.QueryAsync<BoutiqueResponse>(sql, new { request.Location });

        // Return the result as a list
        return boutiques.ToList();
    }
}
