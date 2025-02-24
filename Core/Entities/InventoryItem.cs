using Core.Abstractions;

namespace Core.Entities;

public sealed class InventoryItem(int id, int boutiqueId, string imageUrl, string caption, DateTime timestamp) : BaseEntity
{
    public int Id { get; private set; } = id;
    public int BoutiqueId { get; private set; } = boutiqueId;
    public string ImageUrl { get; internal set; } = imageUrl;
    public string Caption { get; internal set; } = caption;
    public DateTime Timestamp { get; private set; } = timestamp;
}