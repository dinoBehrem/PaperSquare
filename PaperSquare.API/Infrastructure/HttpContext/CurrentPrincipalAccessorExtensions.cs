using PaperSquare.Core.Infrastructure.CurrentUserAccessor;

namespace PaperSquare.API.Infrastructure.HttpContext
{
    public static class CurrentPrincipalAccessorExtensions
    {
        public static IServiceCollection CurrentPrincipalAccessorConfig(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddSingleton<ICurrentPrincipalAccessor, HttpContextCurrentPrincipalAccessor>();

            return services;
        }
    }
}
