using PaperSquare.Core.Application.Features.JWT;
using PaperSquare.Core.Application.Features.UserManagement;

namespace PaperSquare.API.Infrastructure.AppServices;

public static class AppServicesConfiguration
{
    public static IServiceCollection AppServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        return services;
    }
}
