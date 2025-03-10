namespace Application.Features.Boutiques.Responses;

public sealed class BoutiqueResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ProfilePicture { get; init; } = string.Empty;
    public string InstagramLink { get; init; } = string.Empty;
}
