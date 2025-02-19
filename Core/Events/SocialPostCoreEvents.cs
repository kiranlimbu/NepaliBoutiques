using Core.Abstractions;
using Core.Entities;

namespace Core.Events;

public sealed record SocialPostRemovedCoreEvent(SocialPost Post) : ICoreEvent;

