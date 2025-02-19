using Application.Abstractions;

namespace Application.Features.Boutiques.Commands;

public record AddBoutiqueCommand(
    int OwnerId,
    string Name, 
    string ProfilePicture, 
    int Followers, 
    string Description, 
    string Contact, 
    string InstagramLink) : ICommand<int>;