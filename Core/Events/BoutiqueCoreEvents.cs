using Core.Abstractions;
using Core.Entities;

namespace Core.Events;

public sealed record BoutiqueAddedCoreEvent(Boutique Boutique) : ICoreEvent;

public sealed record BoutiqueUpdatedCoreEvent(Boutique Boutique) : ICoreEvent;

public sealed record BoutiqueDeletedCoreEvent(Boutique Boutique) : ICoreEvent;
