using Microsoft.Extensions.DependencyInjection;

namespace PaperSquare.Core.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjector).Assembly));

        return services;
    }
}
