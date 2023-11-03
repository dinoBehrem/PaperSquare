using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Data.Data;
using PaperSquare.Domain.Entities.Identity;

namespace PaperSquare.API.Infrastructure.Auth
{
    public static class AddIdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<PaperSquareDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            });

            return services;
        }
    }
}
