
namespace Core.Entities;

public sealed class Boutique
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ProfilePicture { get; set; } = string.Empty;

    public int Followers { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Contact { get; set; } = string.Empty;

    public string InstagramLink { get; set; } = string.Empty;

    public List<InventoryItem> Inventories { get; set; } = new();
}