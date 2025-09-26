
using HandNote.Data.DTOs.Media_Files;
using HandNote.Data.Results;

namespace HandNote.Data.Interfaces
{
    public interface IMediaFilesRepository
    {
        Task<OperationResult<MediaFilesCreateResponseDto>> CreateMediaFilesAsync(MediaFilesCreateRequestDto dto);
    }
}