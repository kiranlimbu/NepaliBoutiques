namespace Core.Entities;

public sealed class Permission
{
    // predefined permissions
    // Boutique
    public static readonly Permission ViewBoutique = new(1, "View Boutique");
    public static readonly Permission EditBoutique = new(2, "Edit Boutique");
    public static readonly Permission DeleteBoutique = new(3, "Delete Boutique");
    // Inventory
    public static readonly Permission ViewInventory = new(4, "View Inventory");
    public static readonly Permission EditInventory = new(5, "Edit Inventory");
    public static readonly Permission DeleteInventory = new(6, "Delete Inventory");
    
    // Users
    public static readonly Permission ViewUsers = new(10, "View Users");
    public static readonly Permission EditUser = new(11, "Edit User");
    public static readonly Permission DeleteUser = new(12, "Delete User");
    
    // Notifications
    public static readonly Permission ViewNotifications = new(19, "View Notifications");
    public static readonly Permission EditNotification = new(20, "Edit Notification");
    public static readonly Permission DeleteNotification = new(21, "Delete Notification");
    // Settings
    public static readonly Permission ViewSettings = new(22, "View Settings");
    public static readonly Permission EditSettings = new(23, "Edit Settings");
    public static readonly Permission DeleteSettings = new(24, "Delete Settings");
    // Messages
    public static readonly Permission ViewMessages = new(25, "View Messages");
    public static readonly Permission EditMessage = new(26, "Edit Message");
    public static readonly Permission DeleteMessage = new(27, "Delete Message");
    // Reviews
    public static readonly Permission ViewReviews = new(28, "View Reviews");
    public static readonly Permission EditReview = new(29, "Edit Review");
    public static readonly Permission DeleteReview = new(30, "Delete Review");

    public Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }

}

