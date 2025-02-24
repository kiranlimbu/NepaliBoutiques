using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Infrastructure.ExternalServices;
using Application.Abstractions;


namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = 
            configuration.GetConnectionString("DefaultConnection") ?? 
            throw new ArgumentException(nameof(configuration));

        services.AddDbContext<NepaliBoutiqueDbContext>(options => 
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}
