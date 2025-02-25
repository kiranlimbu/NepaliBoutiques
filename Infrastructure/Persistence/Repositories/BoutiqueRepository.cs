using Core.Abstractions.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories;

internal sealed class BoutiqueRepository : BaseRepository<Boutique>, IBoutiqueRepository
{
    public BoutiqueRepository(NepaliBoutiqueDbContext context) : base(context)
    {
    }

    public async Task<Boutique?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<Boutique>()
            .SingleOrDefaultAsync(b => b.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Boutique>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Boutique>().ToListAsync(cancellationToken);
    }
}
