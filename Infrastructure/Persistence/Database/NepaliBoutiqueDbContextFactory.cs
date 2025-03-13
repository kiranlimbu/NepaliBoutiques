using Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Infrastructure.Persistence.Database;
/// <summary>
/// This is used to create a new instance of the NepaliBoutiqueDbContext for design-time migrations.
/// Why?
/// EF Core Needs a Parameterless Constructor 
/// Since our DbContext has dependencies (IDateTimeProvider, IPublisher), EF Core fails to create an instance at design time. 
/// This factory solves this by manually creating an instance.
/// Design-Time Factory Ignores Runtime Dependencies 
/// The factory only initializes required EF Core dependencies (DbContextOptions), skipping IDateTimeProvider and IPublisher (since they're not needed for migrations).
/// Keeps Clean Architecture Intact → You don't need to modify NepaliBoutiqueDbContext, keeping your runtime DI setup unchanged.
/// </summary>
public sealed class NepaliBoutiqueDbContextFactory : IDesignTimeDbContextFactory<NepaliBoutiqueDbContext>
{
    public NepaliBoutiqueDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("Database")
            ?? throw new InvalidOperationException("Connection string 'Database' not found.");

        var optionsBuilder = new DbContextOptionsBuilder<NepaliBoutiqueDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        // Create mock implementations for required dependencies
        var mockDateTimeProvider = new MockDateTimeProvider();
        var mockPublisher = new MockPublisher();

        return new NepaliBoutiqueDbContext(optionsBuilder.Options, mockDateTimeProvider, mockPublisher);
    }
}

// Mock implementations to satisfy DbContext dependencies
internal sealed class MockDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

internal sealed class MockPublisher : IPublisher
{
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        return Task.CompletedTask;
    }

    public Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

