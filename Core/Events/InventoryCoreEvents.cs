using Core.Abstractions;
using Core.Entities;

namespace Core.Events;

public sealed record InventoryItemAddedCoreEvent(InventoryItem item) : ICoreEvent;

public sealed record InventoryItemsAddedCoreEvent(IEnumerable<InventoryItem> items) : ICoreEvent;

public sealed record InventoryItemRemovedCoreEvent(InventoryItem item) : ICoreEvent;

public sealed record InventoryItemsRemovedCoreEvent(IEnumerable<InventoryItem> items) : ICoreEvent;

public sealed record InventoryItemUpdatedCoreEvent(InventoryItem item) : ICoreEvent;

