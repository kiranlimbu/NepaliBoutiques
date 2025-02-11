using Core.Abstractions;
using Core.Entities;

namespace Core.Events;

public sealed record BoutiqueUpdatedCoreEvent(Boutique boutique) : ICoreEvent;