using System.Threading.Tasks;
using HandNote.Data.DTOs.UserSession.Logout;
using HandNote.Data.DTOs.UserSession.Query;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface IUserSessionService
    {
        Task<OperationResult<LogoutUserResponseDto>> LogoutUserAsync(LogoutUserRequestDto dto);
        Task<OperationResult<LogoutAllSessionsResponseDto>> LogoutAllSessionsAsync(LogoutAllSessionsRequestDto dto);
        Task<OperationResult<GetActiveSessionsResponseDto>> GetActiveSessionsAsync(GetActiveSessionsRequestDto dto);
    }
}