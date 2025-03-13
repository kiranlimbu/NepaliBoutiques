using Core.Abstractions.Repositories;
using Core.Entities;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal sealed class SocialPostRepository : BaseRepository<SocialPost>, ISocialPostRepository
{
    public SocialPostRepository(NepaliBoutiqueDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SocialPost>> GetByBoutiqueIdAsync(int boutiqueId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<SocialPost>().Where(sp => sp.BoutiqueId == boutiqueId).ToListAsync(cancellationToken);
    }
}
