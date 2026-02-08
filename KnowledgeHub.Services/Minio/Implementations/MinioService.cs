using KnowledgeHub.Services.Minio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace KnowledgeHub.Services.Minio.Implementations
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClient _minio;
        private readonly string _bucket;
        private readonly int _expiryMinutes;

        public MinioService(IMinioClient minio, IConfiguration config)
        {
            _minio = minio;
            _bucket = config["Minio:Bucket"]
                      ?? throw new ArgumentNullException("Minio:Bucket");
            _expiryMinutes = int.Parse(config["Minio:ExpiryMinutes"] ?? "60");
        }

        public async Task DeleteAsync(string objectName)
        {
            await _minio.RemoveObjectAsync(
            new RemoveObjectArgs()
                .WithBucket(_bucket)
                .WithObject(objectName)
        );
        }

        public async Task<Stream> DownloadAsync(string objectName)
        {
            var memory = new MemoryStream();

            await _minio.GetObjectAsync(
                new GetObjectArgs()
                    .WithBucket(_bucket)
                    .WithObject(objectName)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(memory);
                    })
            );

            memory.Position = 0;
            return memory;
        }

        public async Task<bool> ExistsAsync(string objectName)
        {
            try
            {
                await _minio.StatObjectAsync(
                    new StatObjectArgs()
                        .WithBucket(_bucket)
                        .WithObject(objectName));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task UploadAsync(IFormFile file, string? folder = null)
        {
            var objectName = string.IsNullOrEmpty(folder)
            ? file.FileName
            : $"{folder.TrimEnd('/')}/{file.FileName}";

            await using var stream = file.OpenReadStream();
            await UploadAsync(stream, objectName, file.ContentType);
        }

        public async Task UploadAsync(
        Stream stream,
        string objectName,
        string contentType)
        {
            await EnsureBucketAsync();

            await _minio.PutObjectAsync(
                new PutObjectArgs()
                    .WithBucket(_bucket)
                    .WithObject(objectName)
                    .WithStreamData(stream)
                    .WithObjectSize(stream.Length)
                    .WithContentType(contentType)
            );
        }

        /// <summary>
        /// Create a bucket if it doesn't already exist.
        /// </summary>
        private async Task EnsureBucketAsync()
        {
            var exists = await _minio.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(_bucket));

            if (!exists)
            {
                await _minio.MakeBucketAsync(
                    new MakeBucketArgs().WithBucket(_bucket));
            }
        }
    }
}
