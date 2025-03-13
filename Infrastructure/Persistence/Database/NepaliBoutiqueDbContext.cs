using Microsoft.EntityFrameworkCore;
using Core.Abstractions;
using Application.Abstractions;
using MediatR;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Core.Entities;


namespace Infrastructure.Persistence.Database;
public sealed class NepaliBoutiqueDbContext : DbContext, IUnitOfWork
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPublisher _publisher;
    // we are not using the generic DbContextOptions<T> here because we are not using the DbContextOptionsBuilder<T>
    // also this also allows us to used multiple DbContexts in the future
    public NepaliBoutiqueDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider, IPublisher publisher) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // this is going the scan the assembly and apply all the configurations in the configurations folder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NepaliBoutiqueDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        // Apply IAuditable Configuration to all entities that implement IAuditable
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            .Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreatedAt").IsRequired();
            modelBuilder.Entity(entityType.ClrType).Property<DateTime>("LastModifiedAt").IsRequired();
            modelBuilder.Entity(entityType.ClrType).Property<string>("CreatedBy").IsRequired().HasMaxLength(100);
            modelBuilder.Entity(entityType.ClrType).Property<string>("LastModifiedBy").IsRequired().HasMaxLength(100);
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _dateTimeProvider.UtcNow;
                        // TODO: get the current user
                        entry.Entity.CreatedBy = "System";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = _dateTimeProvider.UtcNow;
                        // TODO: get the current user
                        entry.Entity.LastModifiedBy = "System";
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishCoreEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency error occurred.", ex);
        }
    }

    private async Task PublishCoreEventsAsync()
    {
        var coreEvents = ChangeTracker
            .Entries<BaseEntity>()
            .Select(e => e.Entity)
            .SelectMany(e =>
            {
                var coreEvents = e.GetCoreEvents();
                e.ClearCoreEvents();
                return coreEvents;
            })
            .ToList();

        foreach (var domainEvent in coreEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
