using Core.Entities;

namespace Core.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository that manages InventoryItem entities.
/// </summary>
public interface IInventoryItemRepository
{
    Task<InventoryItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<InventoryItem>> GetByBoutiqueIdAsync(int boutiqueId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<InventoryItem> inventoryItems, CancellationToken cancellationToken = default);
    void Add(InventoryItem inventoryItem);
    void Update(InventoryItem inventoryItem);
    void Delete(int id);
}