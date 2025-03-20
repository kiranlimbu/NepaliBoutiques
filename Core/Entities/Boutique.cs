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

public sealed class Boutique : BaseEntity
{
    // OwnerId is optional because initially the boutique will be created by the system
    public int? OwnerId { get; private set; }
    public string Name { get; private set; }
    public string ProfilePicture { get; private set; }
    public int? Followers { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public string Location { get; private set; }
    public string Contact { get; private set; }
    public string? InstagramLink { get; private set; }
    public List<InventoryItem> Inventories { get; private set; } = [];
    public List<SocialPost> SocialPosts { get; private set; } = [];

    private Boutique(int id, int? ownerId, string name, string profilePicture, int? followers, string description, string category, string location, string contact, string? instagramLink) : base(id)
    {
        OwnerId = ownerId;
        Name = name;
        ProfilePicture = profilePicture;
        Followers = followers;
        Description = description;
        Category = category;
        Location = location;
        Contact = contact;
        InstagramLink = instagramLink;
    }

    /// <summary>
    /// Factory method to create a new Boutique instance.
    /// </summary>
    public static Boutique Create(int id, int? ownerId, string name, string profilePicture, int? followers, string description, string category, string location, string contact, string? instagramLink)
    {
        // Add business logic here: "does this make sense in the context of the business rules?"
        // when creating a boutique through the API, the ownerId is required
        if (ownerId == null || ownerId == 0)
        {
            throw new ArgumentException(BoutiqueErrors.BoutiqueOwnerIdNull.Code, nameof(ownerId));
        }

        // create a boutique
        var boutique = new Boutique(id, ownerId, name, profilePicture, followers, description, category, location, contact, instagramLink);

        // raise the event
        boutique.RaiseCoreEvent(new BoutiqueAddedCoreEvent(boutique));

        // return the boutique
        return boutique;
    }

    /// <summary>
    /// Updates the boutique's properties. Pass null for any property you do not wish to update.
    /// </summary>
    public Result<Boutique> UpdateBoutique(Boutique updatedProperties)
    {
        // apply business logic here
        if (updatedProperties.OwnerId == null || updatedProperties.OwnerId == 0)
        {
            return Result.Failure<Boutique>(BoutiqueErrors.BoutiqueOwnerIdNull);
        }

        // raise the event
        RaiseCoreEvent(new BoutiqueUpdatedCoreEvent(this));

        // return the boutique
        return Result.Success(this);
    }


    /// <summary>
    /// Adds a new inventory item to the boutique's inventory.
    /// </summary>
    /// <param name="item">The inventory item to add.</param>
    /// <returns>A Result indicating the success or failure of the operation.</returns>
    public Result AddInventoryItem(int boutiqueId, string imageUrl, string caption)
    {
        if (boutiqueId == 0)
        {
            return Result.Failure(InventoryErrors.BoutiqueNotFound);
        }

        // apply business logic here

        // add the item to the inventory
        var item = InventoryItem.Create(0, boutiqueId, imageUrl, caption);

        // raise the event
        RaiseCoreEvent(new InventoryItemAddedCoreEvent(item));

        // return the result
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
        // Raise the event
        RaiseCoreEvent(new InventoryItemRemovedCoreEvent(item));

        return Result.Success();
    }



    public Result UpdateInventoryItem(InventoryItem updatedItem)
    {
        var item = Inventories.FirstOrDefault(i => i.Id == updatedItem.Id);
        if (item == null)
        {
            return Result.Failure(InventoryErrors.InventoryItemNotFound);
        }

        // apply business logic here

        // raise the event
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