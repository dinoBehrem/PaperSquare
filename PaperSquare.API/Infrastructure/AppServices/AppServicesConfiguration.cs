using PaperSquare.Infrastructure.Features.Auth;
using PaperSquare.Infrastructure.Features.JWT;
using PaperSquare.Infrastructure.Features.UserManagement;

namespace PaperSquare.API.Infrastructure.AppServices
{
    public static class AppServicesConfiguration
    {
        public static IServiceCollection AppServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
