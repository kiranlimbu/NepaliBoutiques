using Application.Abstractions;
using Application.Features.Boutiques.Responses;
using Core.Abstractions;
using Dapper;

namespace Application.Features.Boutiques.Queries;

internal sealed class SearchBoutiquesByCategoryQueryHandler : IQueryHandler<SearchBoutiquesByCategoryQuery, IReadOnlyList<BoutiqueResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchBoutiquesByCategoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<BoutiqueResponse>>> Handle(SearchBoutiquesByCategoryQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Category))
        {
            return new List<BoutiqueResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
        SELECT 
            Id,
            Name,
            ProfilePicture,
            InstagramLink
        FROM Boutiques 
        WHERE Category = @Category
        """;    

        var boutiques = await connection.QueryAsync<BoutiqueResponse>(sql, new { request.Category });

        return boutiques.ToList();
    }
}
