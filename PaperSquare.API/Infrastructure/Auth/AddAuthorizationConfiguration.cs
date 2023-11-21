using Microsoft.AspNetCore.Authorization;
using PaperSquare.Core.Permissions;

namespace PaperSquare.API.Infrastructure.Auth
{
    public static class AddAuthorizationConfiguration
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                //options.AddPolicy(Permission.FullAccess, builder => builder.RequireRole(Roles.Admin));
                options.AddPolicy(Permission.FullAccess, policy => policy.RequireClaim("role", AppRoles.ADMIN));
                options.AddPolicy(Permission.PartialAccess, builder => builder.RequireClaim("role", AppRoles.ADMIN, AppRoles.EDITOR));
                options.AddPolicy(Permission.RegisteredUser, builder => builder.RequireClaim("role", AppRoles.REGISTERED_USER));
            });

            return services;
        }
    }
}
