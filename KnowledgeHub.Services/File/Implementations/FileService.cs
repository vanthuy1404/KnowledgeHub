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

        public async Task uploadFileAsync(IFormFile file)
        {
            await _minioService.UploadAsync(file, "documents");
        }

        public async Task<Stream> DowloadFileAsync(string fileName)
        {
            var stream = await _minioService.DownloadAsync($"documents/{fileName}");
            return stream;
        }

    }
}
