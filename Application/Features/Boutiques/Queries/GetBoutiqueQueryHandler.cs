using Application.Abstractions;
using Application.Features.Boutiques.Responses;
using Core.Abstractions;
using Dapper;
using Core.Errors;

namespace Application.Features.Boutiques.Queries;

internal sealed class GetBoutiqueQueryHandler : IQueryHandler<GetBoutiqueQuery, BoutiqueResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBoutiqueQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<BoutiqueResponse>> Handle(GetBoutiqueQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
        SELECT 
            Id, 
            Name, 
            ProfilePicture, 
            InstagramLink
        FROM Boutiques 
        WHERE Id = @Id;
        """;

        var boutique = await connection.QueryFirstOrDefaultAsync<BoutiqueResponse>(
            sql, 
            new { request.Id });

        if (boutique is null)
        {
            return Result.Failure<BoutiqueResponse>(BoutiqueErrors.BoutiqueNotFound);
        }

        return boutique;
    }
}
