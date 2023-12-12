using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
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

    public async Task DeleteContainerAsync(string containerName)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        var containerCLient = _blobServiceClient.GetBlobContainerClient(containerName);

        await containerCLient.DeleteIfExistsAsync();
    }

    public async Task<bool> DeleteBlobAsync(string blobName, string containerName)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        if (string.IsNullOrWhiteSpace(blobName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        var blob = containerClient.GetBlobClient(blobName);

        var result = await blob.DeleteIfExistsAsync();

        return result;
    }

    public async Task<string> UploadBlobAsync(string blobName, string containerName, IFormFile file)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        if (string.IsNullOrWhiteSpace(blobName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(blobName);

        using var stream = file.OpenReadStream();

        var result = await blobClient.UploadAsync(stream);

        return blobClient.Uri.ToString();
    }
    
    public async Task<byte[]> DownaloadBlobAsByteArrayAsync(string blobName, string containerName)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        if (string.IsNullOrWhiteSpace(blobName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }

        var container = _blobServiceClient.GetBlobContainerClient(containerName);

        var blob = container.GetBlobClient(blobName);

        using var stream = new MemoryStream();

        await blob.DownloadToAsync(stream);

        return stream.ToArray();
    }
}
