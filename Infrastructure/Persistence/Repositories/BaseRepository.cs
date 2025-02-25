using Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Database;
namespace Infrastructure.Persistence.Repositories;

internal abstract class BaseRepository<T> where T : BaseEntity
{
    protected readonly NepaliBoutiqueDbContext _context;

    protected BaseRepository(NepaliBoutiqueDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public void Add(T entity)
    {
        _context.Add(entity);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void Delete(int id)
    {
        var entity = _context.Set<T>().Find(id);
        if (entity != null)
        {
            _context.Remove(entity);
        }
    }
}
