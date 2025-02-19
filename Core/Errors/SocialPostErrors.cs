using Core.Abstractions;

namespace Core.Errors;

public static class SocialPostErrors
{
    public static Error SocialPostNotFound { get; } = new Error("SocialPost_NotFound", "Post not found");
    public static Error BoutiqueNotFound { get; } = new Error("SocialPost_BoutiqueNotFound", "Boutique not found");
}


