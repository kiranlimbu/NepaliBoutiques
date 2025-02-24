using Application.Abstractions;
using Application.Features.Boutiques.Responses;
using Core.Abstractions;
using Dapper;

namespace Application.Features.Boutiques.Queries;

/// <summary>
/// Handles the query to get boutiques with their inventory items.
/// </summary>
internal sealed class GetBoutiquesWithInventoryQueryHandler : IQueryHandler<GetBoutiquesWithInventoryQuery, IReadOnlyList<BoutiqueWithInventoryResponse>>
{
    // SQL connection factory to create database connections
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBoutiquesWithInventoryQueryHandler"/> class.
    /// </summary>
    /// <param name="sqlConnectionFactory">The SQL connection factory to create database connections.</param>
    public GetBoutiquesWithInventoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    /// <summary>
    /// Handles the query to get boutiques with their inventory items.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A result containing a list of boutiques with their inventory items.</returns>
    public async Task<Result<IReadOnlyList<BoutiqueWithInventoryResponse>>> Handle(GetBoutiquesWithInventoryQuery request, CancellationToken cancellationToken)
    {
        // Create a new database connection
        using var connection = _sqlConnectionFactory.CreateConnection();

        // SQL query to select boutiques and their top 3 inventory items
        const string sql = """
        // SQL query to select boutiques and their top 3 inventory items
        SELECT 
            b.Id,
            b.Name,
            b.ProfilePicture,
            b.Followers,
            b.Description,
            b.Category,
            b.Location,
            b.Contact,
            b.InstagramLink,
            i.Id,
            i.ImageUrl,
            i.Caption,
            i.Timestamp
        FROM Boutiques b
        LEFT JOIN (
            SELECT 
                i1.Id,
                i1.BoutiqueId,
                i1.ImageUrl,
                i1.Caption,
                i1.Timestamp
            FROM InventoryItems i1
            WHERE i1.Id IN (
                SELECT TOP 3 i2.Id  -- Select the top 3 inventory items based on the most recent timestamp
                FROM InventoryItems i2
                WHERE i2.BoutiqueId = i1.BoutiqueId  -- Ensure the inventory items belong to the same boutique
                ORDER BY i2.Timestamp DESC           -- Order by the most recent timestamp
            )
        ) i ON b.Id = i.BoutiqueId  -- Join boutiques with their inventory items
        ORDER BY b.Id               -- Order the results by boutique ID
        OFFSET @Offset ROWS FETCH NEXT 5 ROWS ONLY  -- Pagination: skip a number of rows and fetch the next 5
        """;

        // Execute the query and retrieve boutiques with their inventory items
        var boutiques = await connection.QueryAsync<BoutiqueWithInventoryResponse>(sql);

        // Return the result as a list
        return boutiques.ToList();
    }
}

