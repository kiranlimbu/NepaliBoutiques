namespace API.Models;

public sealed record InventoryItemRequest(
    int? Id, 
    int BoutiqueId,
    string ImageUrl, 
    string Caption);
