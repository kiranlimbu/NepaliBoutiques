using Core.Abstractions;

namespace Core.Errors;

public static class BoutiqueErrors
{
    public static Error BoutiqueNameNull { get; } = new Error("Boutique_Name_Null", "Boutique name cannot be null");
    public static Error BoutiqueProfilePictureNull { get; } = new Error("Boutique_ProfilePicture_Null", "Boutique profile picture cannot be null");
    public static Error BoutiqueNotFound { get; } = new Error("Boutique_NotFound", "Boutique not found");
}

