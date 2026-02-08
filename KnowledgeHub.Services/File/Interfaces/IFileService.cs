using Microsoft.AspNetCore.Http;

namespace KnowledgeHub.Services.File.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string? folder = null);
        Task<Stream> DownloadFileAsync(string objectKey);
    }
}
