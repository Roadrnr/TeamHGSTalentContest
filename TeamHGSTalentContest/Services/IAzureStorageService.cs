using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TeamHGSTalentContest.Services
{
    public interface IAzureStorageService
    {
        Task<string> StoreAndGetFile(string filename, string containerName, IFormFile image);
        Task<string> StoreAndGetFile(string filename, string containerName, string contentType, Stream file);
        Task StoreFile(string filename, IFormFile image);
        Task StoreFile(string filename, string containerName, IFormFile image);
        Task<string> GetFile(string filename, string containerName);
        Task<Stream> StreamFile(string filename, string containerName);
        Task<bool> DeleteFile(string filename, string containerName);
        string GenerateSasToken();
    }
}