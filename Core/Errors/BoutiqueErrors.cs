using Core.Abstractions;

namespace Core.Errors;

public static class BoutiqueErrors
{
    public static Error BoutiqueNameNull { get; } = new Error("Boutique_Name_Null", "Name cannot be null");
    public static Error BoutiqueProfilePictureNull { get; } = new Error("Boutique_ProfilePicture_Null", "Profile picture cannot be null");
    public static Error BoutiqueNotFound { get; } = new Error("Boutique_NotFound", "Not found");
    public static Error BoutiqueAlreadyExists { get; } = new Error("Boutique_Already_Exists", "Already exists");
    public static Error BoutiqueOwnerIdNull { get; } = new Error("Boutique_OwnerId_Null", "Owner ID cannot be null");
}

