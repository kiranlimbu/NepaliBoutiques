using Core.Entities;
using Core.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(NepaliBoutiqueDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<string>> GetUserPermissionsAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>().Where(u => u.Id == userId).SelectMany(u => u.Roles.SelectMany(r => r.Permissions.Select(p => p.Name))).ToListAsync(cancellationToken);
    }
}