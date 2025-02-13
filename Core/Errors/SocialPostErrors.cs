using Core.Abstractions;

namespace Core.Errors;

public static class SocialPostErrors
{
    public static Error SocialPostNotFound { get; } = new Error("SocialPost_NotFound", "Social post not found");
}

