using KnowledgeHub.Services.File.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHub.Api.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <remarks>
        /// folder: optional virtual folder inside bucket (e.g. documents, images/user)
        /// </remarks>
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] IFormFile file,
            [FromQuery] string? folder = null)
        {
            var objectKey = await _fileService.UploadFileAsync(file, folder);

            //  này nên được FE gửi tiếp để BE lưu DB
            return Ok(new
            {
                objectKey
            });
        }

        /// <summary>
        /// Download file by objectKey
        /// </summary>
        [HttpGet("download")]
        public async Task<IActionResult> Download([FromQuery] string objectKey)
        {
            var stream = await _fileService.DownloadFileAsync(objectKey);

            // Lấy tên file từ objectKey để trả về cho browser
            var fileName = Path.GetFileName(objectKey);

            return File(stream, "application/octet-stream", fileName);
        }
    }
}