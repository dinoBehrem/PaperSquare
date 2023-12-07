using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Infrastructure.AzureBlobStorage.Models;

namespace PaperSquare.Infrastructure.AzureBlobStorage.Service;

public sealed class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;

    public AzureBlobStorageService(IOptions<AzureBlobStorageOptions> options)
    {
        _azureBlobStorageOptions = options.Value;

        _blobServiceClient = new BlobServiceClient(_azureBlobStorageOptions.BlobConnection);
    }

    public async Task CreateContainerAsync(string containerName)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();
    }

    public Task DeleteContainerAsync(string containerName)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveBlobAsync(string blobName, string containerName)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();
    }

    public async Task UploadBlobAsync(string blobName, string containerName)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();
    }
}
