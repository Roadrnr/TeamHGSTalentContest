using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace TeamHGSTalentContest.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly IConfiguration _config;
        private CloudBlobContainer _container;

        public AzureStorageService(IConfiguration config)
        {
            _config = config;
            _storageAccount = new CloudStorageAccount(
                new StorageCredentials(_config["Azure:AccountName"], _config["Azure:AccountKey"]), true);
            
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
                    uploadFile = _container.GetBlockBlobReference(filename.GetUniqueName());
                }
            }

            uploadFile.Properties.ContentType = contentType;

            // Create or overwrite the filename blob with the contents of a local file
            // named filename.
            await uploadFile.UploadFromStreamAsync(file);
            //await uploadFile.UploadFromByteArrayAsync(file.FileContents, 0, file.FileContents.Length);
            return uploadFile.Uri.ToString();
        }

        public async Task<string> GetFile(string filename, string containerName)
        {
            var url = $"{_config["Azure:StorageUrl"]}{containerName}/";
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

        public string GenerateSasToken()
        {
            var policy = new SharedAccessAccountPolicy
            {
                // SAS for Blob service only.
                Services = SharedAccessAccountServices.Blob,

                // User has create, read, write, and delete permissions on blobs.
                ResourceTypes = SharedAccessAccountResourceTypes.Object,

                Permissions = SharedAccessAccountPermissions.Read |
                SharedAccessAccountPermissions.Write |
                SharedAccessAccountPermissions.Create |
                SharedAccessAccountPermissions.Delete,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Protocols = SharedAccessProtocol.HttpsOnly
            };
            return _storageAccount.GetSharedAccessSignature(policy);
        }
    }
}
