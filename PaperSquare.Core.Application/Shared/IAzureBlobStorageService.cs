namespace PaperSquare.Core.Application.Shared;

public interface IAzureBlobStorageService
{
    // Methods for BlobServiceClient
    //Task CreateContainerAsync(string containerName);
    //Task DeleteContainerAsync(string containerName);

    // Methods for BlobContainerClient
    Task UploadBlobAsync(string blobName, string containerName);
    Task RemoveBlobAsync(string blobName, string containerName);
}
