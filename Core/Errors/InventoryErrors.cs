using Core.Abstractions;

namespace Core.Errors;

public static class InventoryErrors
{
    public static Error InventoryItemNull { get; } = new Error("InventoryItem_Null", "Item cannot be null");
    public static Error ImageUrlNull { get; } = new Error("InventoryItem_ImageUrl_Null", "Image URL cannot be null");
    public static Error InventoryItemNotFound { get; } = new Error("InventoryItem_NotFound", "Item not found");
    public static Error ItemAlreadyExists { get; } = new Error("InventoryItem_Duplicate", "Item with this url already exists");
    public static Error BoutiqueNotFound { get; } = new Error("Inventory_BoutiqueNotFound", "The boutique associated with the inventory item was not found.");
    public static Error NoItemsAdded { get; } = new Error("Inventory_NoItemsAdded", "No items were added");
    public static Error InventoryItemIdRequired { get; } = new Error("InventoryItem_IdRequired", "Inventory item ID is required");
}


