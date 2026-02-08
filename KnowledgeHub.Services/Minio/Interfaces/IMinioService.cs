using Microsoft.AspNetCore.Http;

namespace KnowledgeHub.Services.Minio.Interfaces
{
    public interface IMinioService
    {
        /// <summary>
        /// Upload from Stream (caller provides the final objectName/key).
        /// </summary>
        Task UploadAsync(Stream stream, string objectName, string contentType);

        /// <summary>
        /// Upload from IFormFile.
        /// - folder: optional prefix inside bucket (virtual folder). Default null = root.
        /// - Returns the final objectName/key actually stored in MinIO (after renaming with _ddMMyyyyHHmmss).
        /// </summary>
        Task<string> UploadAsync(IFormFile file, string? folder = null);

        /// <summary>
        /// Download file by objectName/key.
        /// </summary>
        Task<Stream> DownloadAsync(string objectName);

        /// <summary>
        /// Delete file by objectName/key.
        /// </summary>
        Task DeleteAsync(string objectName);

        /// <summary>
        /// Check if the object exists.
        /// </summary>
        Task<bool> ExistsAsync(string objectName);
    }
}