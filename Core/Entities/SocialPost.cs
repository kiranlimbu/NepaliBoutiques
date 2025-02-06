namespace Core.Entities;

public sealed class SocialPost
{
    public int Id { get; set; }
    public int BoutiqueId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}