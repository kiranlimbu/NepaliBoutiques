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
    // Orders
    public static readonly Permission ViewOrders = new(7, "View Orders");
    public static readonly Permission EditOrder = new(8, "Edit Order");
    public static readonly Permission DeleteOrder = new(9, "Delete Order");
    // Users
    public static readonly Permission ViewUsers = new(10, "View Users");
    public static readonly Permission EditUser = new(11, "Edit User");
    public static readonly Permission DeleteUser = new(12, "Delete User");
    // Social Posts
    public static readonly Permission ViewSocialPosts = new(13, "View Social Posts");
    public static readonly Permission EditSocialPost = new(14, "Edit Social Post");
    public static readonly Permission DeleteSocialPost = new(15, "Delete Social Post");
    // Analytics
    public static readonly Permission ViewAnalytics = new(16, "View Analytics");
    public static readonly Permission EditAnalytics = new(17, "Edit Analytics");
    public static readonly Permission DeleteAnalytics = new(18, "Delete Analytics");
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
    // Coupons
    public static readonly Permission ViewCoupons = new(31, "View Coupons");
    public static readonly Permission EditCoupon = new(32, "Edit Coupon");
    public static readonly Permission DeleteCoupon = new(33, "Delete Coupon");
    // Discounts
    public static readonly Permission ViewDiscounts = new(34, "View Discounts");
    public static readonly Permission EditDiscount = new(35, "Edit Discount");
    public static readonly Permission DeleteDiscount = new(36, "Delete Discount");
    // Promotions
    public static readonly Permission ViewPromotions = new(37, "View Promotions");
    public static readonly Permission EditPromotion = new(38, "Edit Promotion");
    public static readonly Permission DeletePromotion = new(39, "Delete Promotion");
    // Reports
    public static readonly Permission ViewReports = new(40, "View Reports");
    public static readonly Permission EditReport = new(41, "Edit Report");
    public static readonly Permission DeleteReport = new(42, "Delete Report");

    public Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }

}

