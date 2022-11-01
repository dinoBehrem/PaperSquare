using Microsoft.AspNetCore.Authorization;
using PaperSquare.Core.Permissions;

namespace PaperSquare.API.Infrastructure.Auth
{
    public static class AddAuthorizationConfiguration
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                options.AddPolicy(Permission.FullAccess, builder => builder.RequireRole(Roles.Admin));
                options.AddPolicy(Permission.PartialAccess, builder => builder.RequireRole(Roles.Admin, Roles.Editor));
            });

            return services;
        }
    }
}
