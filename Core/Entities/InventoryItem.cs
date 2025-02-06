
namespace Core.Entities;

public sealed class InventoryItem
{
    public int Id { get; set; }
    public int BoutiqueId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}