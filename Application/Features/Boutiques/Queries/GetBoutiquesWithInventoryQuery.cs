using Application.Features.Boutiques.Responses;
using Application.Abstractions;

namespace Application.Features.Boutiques.Queries;

public sealed record GetBoutiquesWithInventoryQuery : IQuery<IReadOnlyList<BoutiqueWithInventoryResponse>>;

