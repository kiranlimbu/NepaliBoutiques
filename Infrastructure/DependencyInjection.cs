using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Infrastructure.ExternalServices;
using Application.Abstractions;
using Infrastructure.Persistence.Repositories;
using Core.Abstractions.Repositories;
using Core.Abstractions;


namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = 
            configuration.GetConnectionString("DefaultConnection") ?? 
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<NepaliBoutiqueDbContext>(options => 
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IBoutiqueRepository, BoutiqueRepository>();
        services.AddScoped<ISocialPostRepository, SocialPostRepository>();
        services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<NepaliBoutiqueDbContext>());
        services.AddSingleton<ISqlConnectionFactory>(sp => new SqlConnectionFactory(connectionString));

        return services;
    }
}
