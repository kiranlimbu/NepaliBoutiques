using Microsoft.EntityFrameworkCore;
using Core.Abstractions;
namespace Infrastructure.Persistence;
public sealed class NepaliBoutiqueDbContext : DbContext, IUnitOfWork
{
    // we are not using the generic DbContextOptions<T> here because we are not using the DbContextOptionsBuilder<T>
    // also this also allows us to used multiple DbContexts in the future
    public NepaliBoutiqueDbContext(DbContextOptions options) : base(options)
    {
    }

//     public DbSet<User> Users { get; set; }
}
