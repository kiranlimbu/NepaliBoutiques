using Core.Abstractions;
using Core.Events;

namespace Core.Entities;

public sealed class Boutique(int id, string name, string profilePicture, int followers, string description, string contact, string instagramLink) : Entity
{
    public int Id { get; private set; } = id;

    public string Name { get; private set; } = name;

    public string ProfilePicture { get; private set; } = profilePicture;

    public int? Followers { get; private set; } = followers;

    public string? Description { get; private set; } = description;

    public string? Contact { get; private set; } = contact;

    public string? InstagramLink { get; private set; } = instagramLink;

    public List<InventoryItem> Inventories { get; private set; } = [];
    public List<SocialPost> SocialPosts { get; private set; } = [];

    /// <summary>
    /// Factory method to create a new Boutique instance.
    /// </summary>
    public static Boutique Create(int id, string name, string profilePicture, int followers, string description, string contact, string instagramLink)
    {
        // Add any validation or initialization logic here
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(profilePicture))
        {
            throw new ArgumentException("Name and profile picture cannot be empty", $"{nameof(name)}, {nameof(profilePicture)}");
        }

        var boutique = new Boutique(id, name, profilePicture, followers, description, contact, instagramLink);
        boutique.RaiseCoreEvent(new BoutiqueAddedCoreEvent(boutique));
        return boutique;
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
            return Result.Failure(InventoryItemErrors.InventoryItemNull);
        }

        // Validate properties of the inventory item
        if (string.IsNullOrWhiteSpace(item.ImageUrl)) 
        {
            return Result.Failure(InventoryItemErrors.ImageUrlNull);
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
            return Result.Failure(InventoryItemErrors.InventoryItemNull);
        }

        foreach (var item in items)
        {
            if (string.IsNullOrWhiteSpace(item.ImageUrl)) 
            {
                return Result.Failure(InventoryItemErrors.ImageUrlNull);
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
           return Result.Failure(InventoryItemErrors.InventoryItemNotFound);
       }
        Inventories.Remove(item);

       return Result.Success();
   }

   /// <summary>
   /// Retrieves an inventory item by its ID.
   /// </summary>
   /// <param name="itemId">The ID of the inventory item to retrieve.</param>
   /// <returns>The inventory item if found; otherwise, null.</returns>
   ///    public InventoryItem? GetInventoryItem(int itemId)
   ///    {
   ///        return Inventories.FirstOrDefault(i => i.Id == itemId) ?? throw new InvalidOperationException("Item not found");
   ///    }

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