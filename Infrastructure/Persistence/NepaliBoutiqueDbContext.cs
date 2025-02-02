using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public sealed class NepaliBoutiqueDbContext : DbContext
{
    public NepaliBoutiqueDbContext(DbContextOptions<NepaliBoutiqueDbContext> options) : base(options)
    {
    }

//     public DbSet<User> Users { get; set; }
}
