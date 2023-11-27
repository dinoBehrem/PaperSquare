using Microsoft.Extensions.DependencyInjection;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Infrastructure.Data.Data;

namespace PaperSquare.Infrastructure.Data;

public static class DependencyInjector
{
    public static IServiceCollection AddDataDependencies(this IServiceCollection services)
    {
        services.AddScoped<IPaperSquareDbContext>(provider => provider.GetService<PaperSquareDbContext>());

        return services;
    }
}
