using Application.Abstractions;
using Application.Features.Boutiques.Responses;

namespace Application.Features.Boutiques.Queries;

public sealed record GetFeaturedBoutiquesQuery : IQuery<IReadOnlyList<BoutiqueResponse>>;

