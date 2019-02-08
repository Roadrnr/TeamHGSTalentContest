using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace TeamHGSTalentContest.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly IConfiguration _config;
        private readonly ILogger<AzureStorageService> _logger;
        private CloudBlobContainer _container;

        public AzureStorageService(IConfiguration config, ILogger<AzureStorageService> logger)
        {
            _config = config;
            _logger = logger;
            _storageAccount = CloudStorageAccount.Parse(_config.GetConnectionString("StorageConnection"));
        }

        private async Task GetContainer(string containerName)
        {
            // Create a blob client.
            var blobClient = _storageAccount.CreateCloudBlobClient();

            // Get a reference to a container named containerName
            _container = blobClient.GetContainerReference(containerName);

            // If "containerName" doesn't exist, create it.
            if (await _container.CreateIfNotExistsAsync())
            {
                await _container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
                _logger.LogInformation($"Container created with name: {containerName}.");
            }
        }

        public async Task StoreFile(string filename, IFormFile file)
        {
            await GetContainer("images");
            // Get a reference to a blob named filename.
            var uploadFile = _container.GetBlockBlobReference(filename);

            if (await uploadFile.ExistsAsync())
            {
                await uploadFile.FetchAttributesAsync();
                if (uploadFile.Properties.Length != file.Length)
                {
                    uploadFile = _container.GetBlockBlobReference(filename.GetUniqueName());
                } else
                {
                    return;
                }
            }

            // Create or overwrite the filename blob with the contents of a local file
            // named filename.
            using (var fileStream = file.OpenReadStream())
            {
                await uploadFile.UploadFromStreamAsync(fileStream);
            }
        }

        public async Task StoreFile(string filename, string container, IFormFile file)
        {
            await GetContainer(container);
            // Get a reference to a blob named filename.
            var uploadFile = _container.GetBlockBlobReference(filename);

            if (await uploadFile.ExistsAsync())
            {
                await uploadFile.FetchAttributesAsync();
                if (uploadFile.Properties.Length != file.Length)
                {
                    uploadFile = _container.GetBlockBlobReference(filename.GetUniqueName());
                } else
                {
                    return;
                }
            }

            // Create or overwrite the filename blob with the contents of a local file
            // named filename.
            using (var fileStream = file.OpenReadStream())
            {
                await uploadFile.UploadFromStreamAsync(fileStream);
            }
        }

        public async Task<string> StoreAndGetFile(string filename, string container, IFormFile file)
        {
            await GetContainer(container);
            // Get a reference to a blob named filename.
            var uploadFile = _container.GetBlockBlobReference(filename);

            if (await uploadFile.ExistsAsync())
            {
                await uploadFile.FetchAttributesAsync();
                if (uploadFile.Properties.Length == file.Length)
                {
                    uploadFile = _container.GetBlockBlobReference(filename.GetUniqueName());
                }
            }

            uploadFile.Properties.ContentType = file.ContentType;

            // Create or overwrite the filename blob with the contents of a local file
            // named filename.
            using (var fileStream = file.OpenReadStream())
            {
                await uploadFile.UploadFromStreamAsync(fileStream);
            }

            return uploadFile.Uri.ToString();
        }

        public async Task<string> StoreAndGetFile(string filename, string container, string contentType, Stream file)
        {
            await GetContainer(container);
            // Get a reference to a blob named filename.
            var uploadFile = _container.GetBlockBlobReference(filename);

            if (await uploadFile.ExistsAsync())
            {
                await uploadFile.FetchAttributesAsync();
                if (uploadFile.Properties.Length == file.Length)
                {
                    var newFileName = filename.GetUniqueName();
                    uploadFile = _container.GetBlockBlobReference(newFileName);
                    _logger.LogInformation($"Upload file renamed to: {newFileName}.");
                }
            }

            uploadFile.Properties.ContentType = contentType;

            // Create or overwrite the filename blob with the contents of a local file
            // named filename.
            await uploadFile.UploadFromStreamAsync(file);
            _logger.LogInformation($"SUCCESS, file uploaded: {uploadFile.Uri}.");
            return uploadFile.Uri.ToString();
        }

        public async Task<string> GetFile(string filename, string containerName)
        {
            var url = $"{_config["BlobService:StorageUrl"]}{containerName}/";
            await GetContainer(containerName);
            // Get a reference to a blob named filename.
            var getFile = _container.GetBlockBlobReference(filename);

            if (await getFile.ExistsAsync()) return $"{url}{filename}";

            var oldFile = $"https://plainstsunami.com/images/{containerName}/{filename}";
            using (var client = new WebClient())
            {
                try
                {
                    using (var stream = client.OpenRead(oldFile))
                    {
                        await getFile.UploadFromStreamAsync(stream);
                    }
                }
                catch (Exception)
                {
                    //Do Nothing
                }
                    
            }
            return $"{url}{filename}";
        }

        public async Task<Stream> StreamFile(string filename, string containerName)
        {
            await GetContainer(containerName);
            var file = _container.GetBlockBlobReference(filename);
            return await file.OpenReadAsync();
        }

        public async Task<bool> DeleteFile(string filename, string containerName)
        {
            await GetContainer(containerName);
            var file = _container.GetBlockBlobReference(filename);
            var result = await file.DeleteIfExistsAsync();
            return result;
        }
    }
}
