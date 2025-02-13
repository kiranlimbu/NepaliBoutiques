using Core.Abstractions;
using Core.Events;
using Core.Errors;
namespace Core.Entities;

/// <summary>
/// Represents the aggregate root for the Boutique entity.
/// The Boutique entity serves as the entry point for managing related entities,
/// such as InventoryItem and SocialPost, ensuring consistency and enforcing
/// business rules across the aggregate.
/// </summary>

public sealed class Boutique : Entity
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string ProfilePicture { get; private set; }

    public int? Followers { get; private set; }

    public string? Description { get; private set; }

    public string? Contact { get; private set; }

    public string? InstagramLink { get; private set; }

    public List<InventoryItem> Inventories { get; private set; } = [];
    public List<SocialPost> SocialPosts { get; private set; } = [];

    private Boutique(int id, string name, string profilePicture, int followers, string description, string contact, string instagramLink) { 
        Id = id;
        Name = name;
        ProfilePicture = profilePicture;
        Followers = followers;
        Description = description;
        Contact = contact;
        InstagramLink = instagramLink;
    }

    /// <summary>
    /// Factory method to create a new Boutique instance.
    /// </summary>
    public static Result<Boutique> Create(int id, string name, string profilePicture, int followers, string description, string contact, string instagramLink)
    {
        // Add any validation or initialization logic here
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Boutique>(BoutiqueErrors.BoutiqueNameNull);
        }

        if (string.IsNullOrWhiteSpace(profilePicture))
        {
            return Result.Failure<Boutique>(BoutiqueErrors.BoutiqueProfilePictureNull);
        }

        var boutique = new Boutique(id, name, profilePicture, followers, description, contact, instagramLink);
        boutique.RaiseCoreEvent(new BoutiqueAddedCoreEvent(boutique));
        return Result.Success(boutique);
    }

    /// <summary>
    /// Updates the boutique's properties. Pass null for any property you do not wish to update.
    /// </summary>
    public Result<Boutique> UpdateBoutique(Boutique updatedProperties)
    {
        Name = updatedProperties.Name ?? Name;
        ProfilePicture = updatedProperties.ProfilePicture ?? ProfilePicture;
        Followers = updatedProperties.Followers ?? Followers;
        Description = updatedProperties.Description ?? Description;
        Contact = updatedProperties.Contact ?? Contact;
        InstagramLink = updatedProperties.InstagramLink ?? InstagramLink;

        // Add any validation or initialization logic here
        if (string.IsNullOrWhiteSpace(Name))
        {
            return Result.Failure<Boutique>(BoutiqueErrors.BoutiqueNameNull);
        }

        if (string.IsNullOrWhiteSpace(ProfilePicture))
        {
            return Result.Failure<Boutique>(BoutiqueErrors.BoutiqueProfilePictureNull);
        }

        RaiseCoreEvent(new BoutiqueUpdatedCoreEvent(this));
        
        return Result.Success(this);
    }

   
   /// <summary>
   /// Adds a new inventory item to the boutique's inventory.
   /// </summary>
   /// <param name="item">The inventory item to add.</param>
   /// <returns>A Result indicating the success or failure of the operation.</returns>
   public Result AddInventoryItem(InventoryItem item)
   {
        if (item == null)
        {
            return Result.Failure(InventoryErrors.InventoryItemNull);
        }

        // Validate properties of the inventory item
        if (string.IsNullOrWhiteSpace(item.ImageUrl)) 
        {
            return Result.Failure(InventoryErrors.ImageUrlNull);
        }

        Inventories.Add(item);
        RaiseCoreEvent(new InventoryItemAddedCoreEvent(item));

        return Result.Success();
   }

   /// <summary>
   /// Adds multiple inventory items to the boutique's inventory.
   /// </summary>
   /// <param name="items">The collection of inventory items to add.</param>
   public Result AddInventoryItems(IEnumerable<InventoryItem> items)
   {
        if (items == null)
        {
            return Result.Failure(InventoryErrors.InventoryItemNull);
        }

        foreach (var item in items)
        {
            if (string.IsNullOrWhiteSpace(item.ImageUrl)) 
            {
                return Result.Failure(InventoryErrors.ImageUrlNull);
            }
            Inventories.Add(item);
        }

        RaiseCoreEvent(new InventoryItemsAddedCoreEvent(items));

        return Result.Success();
   }

   /// <summary>
   /// Removes an inventory item from the boutique's inventory.
   /// </summary>
   /// <param name="itemId">The ID of the inventory item to remove.</param>
   /// <returns>True if the item was removed; otherwise, false.</returns>
   public Result RemoveInventoryItem(int itemId)
   {
       var item = Inventories.FirstOrDefault(i => i.Id == itemId);
       if (item == null)
       {
           return Result.Failure(InventoryErrors.InventoryItemNotFound);
       }
        Inventories.Remove(item);

       return Result.Success();
   }



   public Result UpdateInventoryItem(int itemId, string? newImageUrl = null, string? newCaption = null)
   {
       var item = Inventories.FirstOrDefault(i => i.Id == itemId);
       if (item == null)
       {
           return Result.Failure(InventoryErrors.InventoryItemNotFound);
       }

       // Update properties
       if (newImageUrl != null)
       {
           item.ImageUrl = newImageUrl;
       }
       if (newCaption != null)
       {
           item.Caption = newCaption;
       }

       // Optionally raise an event or perform additional logic
       RaiseCoreEvent(new InventoryItemUpdatedCoreEvent(item));

       return Result.Success();
   }

   /// <summary>
   /// Removes a social post from the boutique's social posts.
   /// </summary>
   /// <param name="postId">The ID of the social post to remove.</param>
   /// <returns>True if the post was removed; otherwise, false.</returns>
   public Result RemoveSocialPost(int postId)
   {
        var post = SocialPosts.FirstOrDefault(p => p.Id == postId);
        if (post == null)
        {
            return Result.Failure(SocialPostErrors.SocialPostNotFound);
        }

        SocialPosts.Remove(post);
        RaiseCoreEvent(new SocialPostRemovedCoreEvent(post));

        return Result.Success();
   }
}