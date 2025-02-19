using Application.Abstractions;
using Application.Features.Boutiques.Responses;
using Core.Abstractions;
using Dapper;

namespace Application.Features.Boutiques.Queries;

/// <summary>
/// Handles the query to retrieve featured boutiques based on recent social media activity.
/// </summary>
internal sealed class GetFeaturedBoutiquesQueryHandler : IQueryHandler<GetFeaturedBoutiquesQuery, IReadOnlyList<BoutiqueResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFeaturedBoutiquesQueryHandler"/> class.
    /// </summary>
    /// <param name="sqlConnectionFactory">The SQL connection factory to create database connections.</param>
    public GetFeaturedBoutiquesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    /// <summary>
    /// Handles the query to get featured boutiques by executing a SQL query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A result containing a list of featured boutique responses.</returns>
    public async Task<Result<IReadOnlyList<BoutiqueResponse>>> Handle(GetFeaturedBoutiquesQuery request, CancellationToken cancellationToken)
    {
        // Create a new database connection
        using var connection = _sqlConnectionFactory.CreateConnection();
        
        // SQL query to select boutiques with recent social media posts
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
            -- Check if there are social posts in the last 14 days for each boutique
            WHERE EXISTS (
                SELECT 1
                FROM SocialPosts sp
                WHERE sp.BoutiqueId = Boutiques.Id
                AND sp.PostDate >= DATEADD(day, -14, GETDATE())
            )
            -- Order results by the most recent social post date in descending order
            ORDER BY (
                SELECT MAX(sp.PostDate)
                FROM SocialPosts sp
                WHERE sp.BoutiqueId = Boutiques.Id
            ) DESC
            -- Fetch only the top 5 rows
            OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY
        """;

        // Execute the query and retrieve the list of featured boutiques
        var boutiques = await connection.QueryAsync<BoutiqueResponse>(sql);

        // Return the result as a list
        return boutiques.ToList();
    }
}
