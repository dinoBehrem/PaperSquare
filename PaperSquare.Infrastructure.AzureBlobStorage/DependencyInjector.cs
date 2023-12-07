﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Infrastructure.AzureBlobStorage.Models;
using PaperSquare.Infrastructure.AzureBlobStorage.Service;

namespace PaperSquare.Infrastructure.AzureBlobStorage;

public static class DependencyInjector
{
    private const string SECTION = "BlobConnection";
    public static IServiceCollection AddAzureBlobStorageDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureBlobStorageOptions>(opt => configuration.GetSection(SECTION));

        services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

        return services;
    }
}
