namespace Core.Entities;

public sealed class BoutiqueWithInventory
{
    public Boutique Boutique { get; set; } = new();
    public List<InventoryItem> Inventories { get; set; } = new();
}
