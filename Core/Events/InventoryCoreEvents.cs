using Core.Abstractions;
using Core.Entities;

namespace Core.Events;

public sealed record InventoryItemAddedCoreEvent(InventoryItem Item) : ICoreEvent;

public sealed record InventoryItemsAddedCoreEvent(IEnumerable<InventoryItem> Items) : ICoreEvent;

public sealed record InventoryItemRemovedCoreEvent(InventoryItem Item) : ICoreEvent;

public sealed record InventoryItemsRemovedCoreEvent(IEnumerable<InventoryItem> Items) : ICoreEvent;

public sealed record InventoryItemUpdatedCoreEvent(InventoryItem Item) : ICoreEvent;

