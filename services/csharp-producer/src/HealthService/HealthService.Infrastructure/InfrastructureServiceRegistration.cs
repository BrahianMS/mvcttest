using Microsoft.Extensions.DependencyInjection;
using HealthService.Infrastructure.EventsBus;

namespace HealthService.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        return services;
    }
}