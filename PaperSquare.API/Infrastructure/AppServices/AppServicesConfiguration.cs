using PaperSquare.Core.Application.Features.JWT;
using PaperSquare.Core.Application.Features.UserManagement;
using PaperSquare.Infrastructure.Features.Auth;

namespace PaperSquare.API.Infrastructure.AppServices;

public static class AppServicesConfiguration
{
    public static IServiceCollection AppServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
