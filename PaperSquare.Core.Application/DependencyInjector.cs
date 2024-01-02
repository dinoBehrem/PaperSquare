using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PaperSquare.API.Middlewares.CommandValidation;
using PaperSquare.Core.Application.Repositories;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using System.Reflection;

namespace PaperSquare.Core.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjector).Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandValidationPipelineBehaviour<,>));

        services.AddValidatorsFromAssembly(typeof(DependencyInjector).Assembly, includeInternalTypes: true);

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
