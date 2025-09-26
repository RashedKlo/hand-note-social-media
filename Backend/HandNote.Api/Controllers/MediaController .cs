using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.Media_Files;
using HandNote.Services.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;

namespace HandNote.Api.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaFilesService _mediaFilesService;
        private readonly ILogger<MediaController> _logger;

        public MediaController(
            IMediaFilesService mediaFilesService,
            ILogger<MediaController> logger)
        {
            _mediaFilesService = mediaFilesService;
            _logger = logger;
        }

        /// <summary>
        /// Upload media files
        /// </summary>
        [HttpPost]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<MediaFilesCreateResponseDto>>> UploadMedia(
            [FromBody] MediaFilesCreateRequestDto request)
        {
            _logger.LogInformation("UploadMedia request initiated");

            var result = await _mediaFilesService.CreateMediaFilesAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("UploadMedia failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Media files uploaded successfully - Count: {FileCount}",
                result.Data?.TotalMediaFiles ?? 0);

            return Ok(new SuccessResponse<MediaFilesCreateResponseDto>(result.Data!, result.Message));
        }
    }
}