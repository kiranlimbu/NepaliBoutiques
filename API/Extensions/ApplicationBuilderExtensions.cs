using API.Middleware;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

/// <summary>
/// Extension methods for IApplicationBuilder to handle database migrations.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Applies any pending database migrations at application startup.
    /// This ensures the database schema is up-to-date with the latest migration.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance.</param>
    /// <remarks>
    /// This method:
    /// 1. Creates a new service scope to resolve scoped services
    /// 2. Gets the DbContext from the service provider
    /// 3. Executes any pending migrations against the database
    /// 
    /// Should be called during application startup after services are configured.
    /// </remarks>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NepaliBoutiqueDbContext>();
        context.Database.Migrate();
    }

    /// <summary>
    /// Adds exception handling middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance.</param>
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}