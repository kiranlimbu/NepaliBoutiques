using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => 
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        // Add services to the container. eg. if you have a service class, you can add it here.

        return services;
    }
}
