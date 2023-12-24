using PaperSquare.Infrastructure.Data.Data;
using PaperSquare.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace PaperSquare.API;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<PublishDomainEventsInterceptor>();
        services.AddScoped<PaperSquareSaveChangesInterceptor>();

        services.AddDbContext<PaperSquareDbContext>((serviceProvider, options) =>
        {
            var publishDomainEventsInterceptor = serviceProvider.GetService<PublishDomainEventsInterceptor>();
            var saveChangesInterceptor = serviceProvider.GetService<PaperSquareSaveChangesInterceptor>()!;

            options.UseNpgsql(configuration.GetConnectionString("DevBase"));
            options.AddInterceptors(publishDomainEventsInterceptor, saveChangesInterceptor);
        });

        return services;
    }
}
