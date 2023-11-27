using Microsoft.Extensions.DependencyInjection;
using PaperSquare.Core.Application.Repositories;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Core.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjector).Assembly));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
