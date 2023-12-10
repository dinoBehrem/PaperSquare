using Microsoft.AspNetCore.Http;

namespace PaperSquare.Core.Application.Shared;

public interface IAzureBlobStorageService
{
    // Methods for BlobServiceClient
    Task CreateContainerAsync(string containerName);
    Task DeleteContainerAsync(string containerName);

    // Methods for BlobContainerClient
    Task<string> UploadBlobAsync(string blobName, string containerName, IFormFile file);
    Task<bool> DeleteBlobAsync(string blobName, string containerName);
}
