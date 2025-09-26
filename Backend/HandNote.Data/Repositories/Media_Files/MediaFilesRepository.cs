using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Media_Files;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.Media_Files.Commands;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Media_Files
{

    public class MediaFilesRepository : IMediaFilesRepository
    {
        private readonly ILogger<MediaFilesRepository> _logger;
        public MediaFilesRepository(ILogger<MediaFilesRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<OperationResult<MediaFilesCreateResponseDto>> CreateMediaFilesAsync(MediaFilesCreateRequestDto dto)
        {
            return await CreateMediaFilesCommand.ExecuteAsync(dto, _logger);
        }
    }
}