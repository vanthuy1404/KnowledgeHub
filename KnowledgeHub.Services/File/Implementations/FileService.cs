using KnowledgeHub.Services.File.Interfaces;
using KnowledgeHub.Services.Minio.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KnowledgeHub.Services.File.Implementations
{
    public class FileService : IFileService
    {
        private readonly IMinioService _minioServiceơ;
        public FileService(IMinioService minioServiceơ)
        {
            _minioServiceơ = minioServiceơ;
        }

        public async Task uploadFileAsync(IFormFile file)
        {
            await _minioServiceơ.UploadAsync(file, "documents");
        }

        public async Task<Stream> DowloadFileAsync(string fileName)
        {
            var stream = await _minioServiceơ.DownloadAsync($"documents/{fileName}");
            return stream;
        }

    }
}
