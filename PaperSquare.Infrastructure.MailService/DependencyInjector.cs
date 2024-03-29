﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Infrastructure.MailService;

namespace PaperSquare.Infrastructure.Data;

public static class DependencyInjector
{
    private const string SECTION = "EmailOptions";
    public static IServiceCollection AddMailDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(opt => configuration.GetSection(SECTION).Bind(opt));
        services.AddScoped<IMailService, MailService.Service.MailService>();

        return services;
    }
}
