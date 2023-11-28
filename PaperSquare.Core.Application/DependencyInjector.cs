using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PaperSquare.Core.Application.Repositories;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using System.Reflection;

namespace PaperSquare.Core.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjector).Assembly));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
