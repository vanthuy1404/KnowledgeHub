using KnowledgeHub.Services.File.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHub.Api.Controllers
{
    [Route("api/file")]
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            await _fileService.uploadFileAsync(file);
            return Ok();
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download([FromRoute] string fileName)
        {
            var stream = await _fileService.DowloadFileAsync(fileName);
            return File(stream, "application/octet-stream", fileName);
        }

    }
}
