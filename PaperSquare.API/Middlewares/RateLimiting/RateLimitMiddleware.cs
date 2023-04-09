using AspNetCoreRateLimit;

namespace PaperSquare.API.Middlewares.RateLimiting
{
    public static class RateLimitMiddleware
    {
        private const string SECTION = "IpRateLimitingSettings";

        internal static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(opt => configuration.GetSection(SECTION).Bind(opt));

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.AddInMemoryRateLimiting();

            return services;
        }
    }
}
