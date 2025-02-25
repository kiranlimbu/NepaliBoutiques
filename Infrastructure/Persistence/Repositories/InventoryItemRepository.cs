using Core.Abstractions.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories;

internal sealed class InventoryItemRepository : BaseRepository<InventoryItem>, IInventoryItemRepository
{
    public InventoryItemRepository(NepaliBoutiqueDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<InventoryItem>> GetByBoutiqueIdAsync(int boutiqueId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<InventoryItem>().Where(i => i.BoutiqueId == boutiqueId).ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<InventoryItem> inventoryItems, CancellationToken cancellationToken = default)
    {
        await _context.Set<InventoryItem>().AddRangeAsync(inventoryItems, cancellationToken);
    }
}
