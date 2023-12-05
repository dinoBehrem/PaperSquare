using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaperSquare.Infrastructure.AzureBlobStorage.Models;

namespace PaperSquare.Infrastructure.AzureBlobStorage;

public static class DependencyInjector
{
    private const string SECTION = "BlobConnection";
    public static IServiceCollection AddAzureBlobStorageDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureBlobStorageOptions>(opt => configuration.GetSection(SECTION));

        return services;
    }
}
