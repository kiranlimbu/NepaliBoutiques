using Core.Entities;

namespace Core.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository that manages InventoryItem entities.
/// </summary>
public interface IInventoryItemRepository
{
    Task<InventoryItem?> GetByIdAsync(int id);
    Task<IEnumerable<InventoryItem>> GetByBoutiqueIdAsync(int boutiqueId);
    void Add(InventoryItem inventoryItem);
    Task AddRangeAsync(IEnumerable<InventoryItem> inventoryItems);
    void Update(InventoryItem inventoryItem);
    void Delete(int id);
}