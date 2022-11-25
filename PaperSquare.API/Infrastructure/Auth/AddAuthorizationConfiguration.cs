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

                //options.AddPolicy(Permission.FullAccess, builder => builder.RequireRole(Roles.Admin));
                options.AddPolicy(Permission.FullAccess, policy => policy.RequireClaim("role", Roles.Admin));
                options.AddPolicy(Permission.PartialAccess, builder => builder.RequireClaim("role", Roles.Admin, Roles.Editor));
                options.AddPolicy(Permission.RegisteredUser, builder => builder.RequireClaim("role", Roles.RegisteredUser));
            });

            return services;
        }
    }
}
