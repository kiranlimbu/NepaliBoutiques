using Core.Abstractions;

namespace Core.Entities;

public sealed class InventoryItem : BaseEntity
{
    public int BoutiqueId { get; private set; }
    // image of 
    public string ImageUrl { get; internal set; }
    public string Caption { get; internal set; }

    private InventoryItem(int id, int boutiqueId, string imageUrl, string caption) : base(id)
    {
        BoutiqueId = boutiqueId;
        ImageUrl = imageUrl;
        Caption = caption;
    }

    public static InventoryItem Create(int id, int boutiqueId, string imageUrl, string caption)
    {
        var newInventoryItem = new InventoryItem(id, boutiqueId, imageUrl, caption);

        return newInventoryItem;
    }
    
}