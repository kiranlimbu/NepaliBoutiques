using Application.Abstractions;
using Application.Features.Inventories.Responses;

namespace Application.Features.Inventories.Queries;

public sealed record GetInventoryForBoutiqueQuery(int BoutiqueId, int Offset, int Limit) : IQuery<IReadOnlyList<InventoryItemResponse>>;


