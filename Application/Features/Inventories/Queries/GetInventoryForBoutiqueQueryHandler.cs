using Application.Abstractions;
using Application.Features.Inventories.Responses;
using Core.Abstractions;
using Dapper;

namespace Application.Features.Inventories.Queries;

internal sealed class GetInventoryForBoutiqueQueryHandler : IQueryHandler<GetInventoryForBoutiqueQuery, IReadOnlyList<InventoryItemResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInventoryForBoutiqueQueryHandler"/> class.
    /// </summary>
    /// <param name="sqlConnectionFactory">The SQL connection factory to create database connections.</param>
    public GetInventoryForBoutiqueQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    /// <summary>
    /// Handles the query to retrieve inventory items for a specific boutique.
    /// </summary>
    /// <param name="request">The query request containing the boutique ID, offset, and limit for pagination.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A result containing a list of inventory item responses for the specified boutique.</returns>
    public async Task<Result<IReadOnlyList<InventoryItemResponse>>> Handle(GetInventoryForBoutiqueQuery request, CancellationToken cancellationToken)
    {
        // Create a new database connection
        using var connection = _sqlConnectionFactory.CreateConnection();

        // SQL query to select inventory items for a specific boutique
        const string sql = """
            SELECT 
                Id,
                ImageUrl,
                Caption,
                CreatedAt
            FROM InventoryItems
            WHERE BoutiqueId = @BoutiqueId
            ORDER BY CreatedAt DESC
            OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY
        """;

        // Execute the query with the provided boutique ID and retrieve matching inventory items
        var inventoryItems = await connection.QueryAsync<InventoryItemResponse>(
            sql, 
            new {
                request.BoutiqueId, 
                request.Offset, 
                request.Limit 
            }
        );
    
        // Return the result as a list
        return inventoryItems.ToList();
    }
}
