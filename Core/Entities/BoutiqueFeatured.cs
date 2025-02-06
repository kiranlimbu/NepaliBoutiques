namespace Core.Entities;

public sealed class BoutiqueFeatured
{
    public BoutiqueWithInventory BoutiqueWithInventory { get; set; } = new();
    public SocialPost SocialPost { get; set; } = new();
}