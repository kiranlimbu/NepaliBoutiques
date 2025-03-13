using API.Models;

namespace API.Models;

public sealed record BoutiqueWithInventoryRequest(
    int Id, 
    string Name, 
    string ProfilePicture, 
    int Followers, 
    string Description, 
    string Contact, 
    string InstagramLink, 
    List<InventoryItemRequest> InventoryItems);
