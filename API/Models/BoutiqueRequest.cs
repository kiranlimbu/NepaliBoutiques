namespace API.Models;

public sealed record BoutiqueRequest(
    int? Id, 
    int OwnerId, 
    string Name, 
    string ProfilePicture, 
    int? Followers, 
    string Description, 
    string Category, 
    string Location, 
    string Contact, 
    string? InstagramLink);