using PaperSquare.Infrastructure.Features.Auth;
using PaperSquare.Infrastructure.Features.JWT;

namespace PaperSquare.API.Infrastructure.AppServices
{
    public static class AppServicesConfiguration
    {
        public static IServiceCollection AppServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
