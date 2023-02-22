using Microsoft.OpenApi.Models;
using PaperSquare.API.Infrastructure.Versioning;

namespace PaperSquare.API.Infrastructure.SwaggerGen
{
    public static class SwaggerGenConfiguration
    {
        public static IServiceCollection SwaggerGenConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc(ApiVersions.V_1, new OpenApiInfo()
                {
                    Title = "PaperSquare.API",
                    Version = ApiVersions.V_1,
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                      {
                          Reference = new OpenApiReference
                          {
                              Type = ReferenceType.SecurityScheme,
                              Id = "Bearer"
                          }
                      },
                      new string[] {}
                   }
                 });
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            return services;
        }
    }
}
