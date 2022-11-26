using PaperSquare.Infrastructure.Features.Auth;
using PaperSquare.Infrastructure.Features.JWT;
using PaperSquare.Infrastructure.Features.UserManagement;

namespace PaperSquare.API.Infrastructure.AppServices
{
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
}
