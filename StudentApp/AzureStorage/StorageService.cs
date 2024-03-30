using Azure.Storage.Blobs;
using StudentApp.Configurations;

namespace StudentApp.AzureStorage;

public class StorageService : IStorageService 
{ 
    private readonly BlobServiceClient _blobServiceClient; 
    private readonly IConfiguration _configuration; 
    
    public StorageService(BlobServiceClient blobServiceClient, IConfiguration configuration) 
    { 
        _blobServiceClient = blobServiceClient;     
        _configuration = configuration;
    }
    
    public void Upload(IFormFile formFile) 
    {
        var containerName = _configuration.Get<AppConfig>().Storage.ContainerName;
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(formFile.FileName);
        using var stream = formFile.OpenReadStream();
        blobClient.Upload(stream, true);
    }
}
