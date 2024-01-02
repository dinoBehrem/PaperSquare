using PaperSquare.API.Middlewares.Exceptions;

namespace PaperSquare.API.Infrastructure.Middlewares
{
    public static class ExceptionMiddlewareExtension
    {
        public static IServiceCollection AddExceptionConfig(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseMiddlewareHandlers(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}
