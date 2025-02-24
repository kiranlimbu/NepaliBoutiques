using Application.Abstractions;

namespace Application.Features.Boutiques.Commands;

public record UpdateBoutiqueCommand(
    int Id,
    int OwnerId,
    string Name,
    string ProfilePicture,
    int? Followers,
    string Description,
    string Category,
    string Location,
    string Contact,
    string? InstagramLink) : ICommand;