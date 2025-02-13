using Core.Abstractions;

namespace Core.Errors;

public static class InventoryErrors
{
    public static Error InventoryItemNull { get; } = new Error("InventoryItem_Null", "Inventory item cannot be null");
    public static Error ImageUrlNull { get; } = new Error("ImageUrl_Null", "Image URL cannot be null");
    public static Error InventoryItemNotFound { get; } = new Error("InventoryItem_NotFound", "Inventory item not found");
}


