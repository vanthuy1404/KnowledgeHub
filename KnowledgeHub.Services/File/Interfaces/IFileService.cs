using Microsoft.AspNetCore.Http;

namespace KnowledgeHub.Services.File.Interfaces
{
    public interface IFileService
    {
        Task uploadFileAsync(IFormFile file);
        Task<Stream> DowloadFileAsync(string fileName);
    }
}
