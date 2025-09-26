using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Media_Files;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface IMediaFilesService
    {
        Task<OperationResult<MediaFilesCreateResponseDto>> CreateMediaFilesAsync(MediaFilesCreateRequestDto dto);

    }
}