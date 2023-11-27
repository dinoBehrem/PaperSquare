using PaperSquare.Infrastructure.Data.Data;
using PaperSquare.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace PaperSquare.API;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<PaperSquareSaveChangesInterceptor>();
        services.AddDbContext<PaperSquareDbContext>((serviceProvider, options) =>
        {
            var saveChangesInterceptor = serviceProvider.GetService<PaperSquareSaveChangesInterceptor>()!;

            options.UseNpgsql(configuration.GetConnectionString("DevBase"));
            options.AddInterceptors(saveChangesInterceptor);
        });

        return services;
    }
}
