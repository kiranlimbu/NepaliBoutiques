using Core.Abstractions;

namespace Core.Entities;

public sealed class InventoryItem(int id, int boutiqueId, string imageUrl, string caption, DateTime timestamp) : Entity
{
    public int Id { get; private set; } = id;
    public int BoutiqueId { get; private set; } = boutiqueId;
    public string ImageUrl { get; private set; } = imageUrl;
    public string Caption { get; private set; } = caption;
    public DateTime Timestamp { get; private set; } = timestamp;

    /// <summary>
    /// Updates the inventory item's properties. Pass null for any property you do not wish to update.
    /// </summary>
    public void UpdateInventoryItem(string? newImageUrl=null, string? newCaption=null, DateTime? newTimestamp=null)
    {
        ImageUrl = newImageUrl ?? ImageUrl;
        Caption = newCaption ?? Caption;
        Timestamp = newTimestamp ?? Timestamp;
    }
}