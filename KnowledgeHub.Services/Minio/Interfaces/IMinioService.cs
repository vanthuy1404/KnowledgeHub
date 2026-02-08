using Microsoft.AspNetCore.Http;

namespace KnowledgeHub.Services.Minio.Interfaces
{
    public interface IMinioService
    {
        /// <summary>
        /// Upload file
        /// </summary>
        Task UploadAsync(Stream stream, string objectName, string contentType);
        /// <summary>
        /// Upload từ IFormFile
        /// </summar
        Task UploadAsync(IFormFile file, string? folder = null);
        /// <summary>
        /// Download file
        /// </summary>
        Task<Stream> DownloadAsync(string objectName);
        /// <summary>
        /// Delete file
        /// </summary>
        Task DeleteAsync(string objectName);
        /// <summary>
        /// Check if the file exists.
        /// </summary>
        Task<bool> ExistsAsync(string objectName);
    }
}
