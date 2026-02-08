using System.Text.RegularExpressions;
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
            _bucket = config["Minio:Bucket"] ?? throw new ArgumentNullException("Minio:Bucket");
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
                    .WithCallbackStream(stream => stream.CopyTo(memory))
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

        // folder mặc định null
        public async Task<string> UploadAsync(IFormFile file, string? folder = null)
        {
            await EnsureBucketAsync();

            // 1) sanitize filename
            var originalSafe = Path.GetFileName(file.FileName); 
            var nameNoExt = Path.GetFileNameWithoutExtension(originalSafe);
            var ext = Path.GetExtension(originalSafe); // giữ .jpg .png .pdf ...

            nameNoExt = SanitizeKeyPart(nameNoExt);

            // thêm timestamp ddMMyyyyHHmmss
            var stamp = DateTime.UtcNow.ToString("ddMMyyyyHHmmss");
            var newFileName = $"{nameNoExt}_{stamp}{ext}";

            // sanitize folder + ghép key
            var objectName = BuildObjectKey(folder, newFileName);

            await using var stream = file.OpenReadStream();
            await _minio.PutObjectAsync(
                new PutObjectArgs()
                    .WithBucket(_bucket)
                    .WithObject(objectName)
                    .WithStreamData(stream)
                    .WithObjectSize(stream.Length)
                    .WithContentType(string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType)
            );

            // trả ra objectName để bạn lưu DB / trả về client
            return objectName;
        }

        // Nếu bạn vẫn muốn giữ overload Stream như cũ:
        public async Task UploadAsync(Stream stream, string objectName, string contentType)
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

        private static string BuildObjectKey(string? folder, string fileName)
        {
            if (string.IsNullOrWhiteSpace(folder))
                return fileName;

            // chuẩn hoá dấu /
            folder = folder.Trim().Replace("\\", "/").Trim('/');

            // làm sạch từng segment folder (tránh ../, ký tự lạ)
            var segments = folder.Split('/', StringSplitOptions.RemoveEmptyEntries)
                                 .Select(SanitizeKeyPart);

            var safeFolder = string.Join("/", segments);

            return $"{safeFolder}/{fileName}";
        }

        private static string SanitizeKeyPart(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "file";

            // Cho phép: chữ, số, -, _, .
            // thay phần còn lại thành "-"
            input = Regex.Replace(input, @"[^a-zA-Z0-9\-_.]+", "-");

            // tránh chuỗi dài/đầu-cuối dấu -
            input = input.Trim('-');

            return string.IsNullOrWhiteSpace(input) ? "file" : input;
        }

        private async Task EnsureBucketAsync()
        {
            var exists = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucket));
            if (!exists)
                await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucket));
        }
    }
}
