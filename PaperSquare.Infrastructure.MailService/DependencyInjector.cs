using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaperSquare.Infrastructure.MailService;
using PaperSquare.Infrastructure.MailService.Service;

namespace PaperSquare.Infrastructure.Data;

public static class DependencyInjector
{
    private const string SECTION = "EmailConfiguration";
    public static IServiceCollection AddMailDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailConfiguration>(opt => configuration.GetSection(SECTION).Bind(opt));
        services.AddScoped<IMailService, MailService.Service.MailService>();
        return services;
    }
}
