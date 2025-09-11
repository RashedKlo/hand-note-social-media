using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HandNote.Data.DTOs.Media_Files;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;

namespace HandNote.Services.MediaFiles
{
    public sealed class MediaFilesService : IMediaFilesRepository
    {
        private readonly IMediaFilesRepository _mediaFilesRepository;
        private readonly ILogger<MediaFilesService> _logger;

        public MediaFilesService(IMediaFilesRepository mediaFilesRepository, ILogger<MediaFilesService> logger)
        {
            _mediaFilesRepository = mediaFilesRepository ?? throw new ArgumentNullException(nameof(mediaFilesRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<MediaFilesCreateResponseDto>> CreateMediaFilesAsync(MediaFilesCreateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Media files creation attempted with null data");
                return OperationResult<MediaFilesCreateResponseDto>.Failure("Media files data is required");
            }

            _logger.LogInformation("Processing media files creation for UserId: {UserId}, FileCount: {FileCount}",
                dto.UserId, dto.FilePaths?.Count ?? 0);

            // Create media files via repository
            var createResult = await _mediaFilesRepository.CreateMediaFilesAsync(dto);

            if (!createResult.IsSuccess)
            {
                _logger.LogWarning("Media files creation failed for UserId: {UserId}: {Message}",
                    dto.UserId, createResult.Message);
                return OperationResult<MediaFilesCreateResponseDto>.Failure(createResult.Message);
            }

            _logger.LogInformation("Media files created successfully - UserId: {UserId}, RecordsInserted: {RecordsInserted}",
                createResult.Data?.UserId, createResult.Data?.RecordsInserted);

            return OperationResult<MediaFilesCreateResponseDto>.Success(createResult.Data!, createResult.Message);
        }
    }
}