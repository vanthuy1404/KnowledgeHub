using KnowledgeHub.Services.File.Interfaces;
using KnowledgeHub.Services.Minio.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KnowledgeHub.Services.File.Implementations
{
    public class FileService : IFileService
    {
        private readonly IMinioService _minioService;
        public FileService(IMinioService minioService)
        {
            _minioService = minioService;
        }

        // folder truyền vào, default null
        public async Task<string> UploadFileAsync(IFormFile file, string? folder = null)
        {
            return await _minioService.UploadAsync(file, folder);
        }

        // nên download theo objectKey (đầy đủ) thay vì chỉ fileName
        public async Task<Stream> DownloadFileAsync(string objectKey)
        {
            return await _minioService.DownloadAsync(objectKey);
        }
    }
}