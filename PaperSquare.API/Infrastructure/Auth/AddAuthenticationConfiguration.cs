using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PaperSquare.Infrastructure.Features.JWT;
using System.Text;

namespace PaperSquare.API.Infrastructure.Auth
{
    public static class AddAuthenticationConfiguration
    {
        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurationSection = configuration.GetSection(nameof(TokenConfiguration));
            var tokenConfiguration = tokenConfigurationSection.Get<TokenConfiguration>();

            services.Configure<TokenConfiguration>(tokenConfigurationSection);

            services.Configure<TokenConfiguration>(options =>
            {
                options.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfiguration.SecretKey));
                options.SigningCredentials = new SigningCredentials(options.SecurityKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    RequireExpirationTime = true
                };
            });

            return services;
        }
    }
}
