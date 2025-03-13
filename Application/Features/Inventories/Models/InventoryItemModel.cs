namespace Application.Features.Inventories.Models;

public sealed record InventoryItemModel(
    int? Id,
    int BoutiqueId,
    string ImageUrl,
    string Caption);
