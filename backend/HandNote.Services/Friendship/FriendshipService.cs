using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship;
using HandNote.Data.DTOs.Friendship.Create;
using HandNote.Data.DTOs.Friendship.Update;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using HandNote.Data.Interfaces;
using Microsoft.Extensions.Logging;
using HandNote.Data.DTOs.Friendship.Queries;

namespace HandNote.Services.Friendship
{
    public sealed class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<FriendshipService> _logger;

        public FriendshipService(
            IFriendshipRepository friendshipRepository,
            IUserRepository userRepository,
            ILogger<FriendshipService> logger)
        {
            _friendshipRepository = friendshipRepository ?? throw new ArgumentNullException(nameof(friendshipRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<FriendshipCreateResponseDto>> SendFriendRequestAsync(FriendshipCreateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Friend request sending attempted with null data");
                return OperationResult<FriendshipCreateResponseDto>.Failure("Friendship data is required");
            }

            _logger.LogInformation("Processing friend request sending for RequesterId: {RequesterId}, AddresseeId: {AddresseeId}",
                dto.RequesterId, dto.AddresseeId);

            var validationResult = await FriendshipValidation.ValidateFriendRequestCreationAsync(dto, _userRepository, _friendshipRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Friend request sending validation failed for RequesterId {RequesterId}, AddresseeId {AddresseeId}: {Error}",
                    dto.RequesterId, dto.AddresseeId, validationResult.Message);
                return OperationResult<FriendshipCreateResponseDto>.Failure(validationResult.Message);
            }

            var createResult = await _friendshipRepository.AddFriendAsync(dto);

            if (!createResult.IsSuccess)
            {
                _logger.LogWarning("Friend request sending failed for RequesterId {RequesterId}, AddresseeId {AddresseeId}: {Message}",
                    dto.RequesterId, dto.AddresseeId, createResult.Message);
                return OperationResult<FriendshipCreateResponseDto>.Failure(createResult.Message);
            }

            _logger.LogInformation("Friend request sent successfully - FriendshipId: {FriendshipId}, RequesterId: {RequesterId}, AddresseeId: {AddresseeId}",
                createResult.Data?.FriendshipId, dto.RequesterId, dto.AddresseeId);

            return OperationResult<FriendshipCreateResponseDto>.Success(createResult.Data!, createResult.Message);
        }

        public async Task<OperationResult<FriendshipUpdateResponseDto>> AcceptFriendRequestAsync(FriendshipUpdateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Friend request acceptance attempted with null data");
                return OperationResult<FriendshipUpdateResponseDto>.Failure("Friendship data is required");
            }

            // Override status to accepted

            _logger.LogInformation("Processing friend request acceptance for FriendshipId: {FriendshipId}, UserId: {UserId}",
                dto.FriendshipId, dto.UserId);

            var validationResult = await FriendshipValidation.ValidateFriendshipStatusUpdateAsync(dto, _userRepository, _friendshipRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Friend request acceptance validation failed for FriendshipId {FriendshipId}, UserId {UserId}: {Error}",
                    dto.FriendshipId, dto.UserId, validationResult.Message);
                return OperationResult<FriendshipUpdateResponseDto>.Failure(validationResult.Message);
            }

            var updateResult = await _friendshipRepository.UpdateFriendAsync(dto);

            if (!updateResult.IsSuccess)
            {
                _logger.LogWarning("Friend request acceptance failed for FriendshipId {FriendshipId}, UserId {UserId}: {Message}",
                    dto.FriendshipId, dto.UserId, updateResult.Message);
                return OperationResult<FriendshipUpdateResponseDto>.Failure(updateResult.Message);
            }

            _logger.LogInformation("Friend request accepted successfully - FriendshipId: {FriendshipId}, UserId: {UserId}",
                dto.FriendshipId, dto.UserId);

            return OperationResult<FriendshipUpdateResponseDto>.Success(updateResult.Data!, updateResult.Message);
        }

        public async Task<OperationResult<FriendshipUpdateResponseDto>> DeclineFriendRequestAsync(FriendshipUpdateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Friend request decline attempted with null data");
                return OperationResult<FriendshipUpdateResponseDto>.Failure("Friendship data is required");
            }

            // Override status to declined

            _logger.LogInformation("Processing friend request decline for FriendshipId: {FriendshipId}, UserId: {UserId}",
                dto.FriendshipId, dto.UserId);

            var validationResult = await FriendshipValidation.ValidateFriendshipStatusUpdateAsync(dto, _userRepository, _friendshipRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Friend request decline validation failed for FriendshipId {FriendshipId}, UserId {UserId}: {Error}",
                    dto.FriendshipId, dto.UserId, validationResult.Message);
                return OperationResult<FriendshipUpdateResponseDto>.Failure(validationResult.Message);
            }

            var updateResult = await _friendshipRepository.UpdateFriendAsync(dto);

            if (!updateResult.IsSuccess)
            {
                _logger.LogWarning("Friend request decline failed for FriendshipId {FriendshipId}, UserId {UserId}: {Message}",
                    dto.FriendshipId, dto.UserId, updateResult.Message);
                return OperationResult<FriendshipUpdateResponseDto>.Failure(updateResult.Message);
            }

            _logger.LogInformation("Friend request declined successfully - FriendshipId: {FriendshipId}, UserId: {UserId}",
                dto.FriendshipId, dto.UserId);

            return OperationResult<FriendshipUpdateResponseDto>.Success(updateResult.Data!, updateResult.Message);
        }

        public async Task<OperationResult<FriendshipUpdateResponseDto>> CancelFriendRequestAsync(FriendshipUpdateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Friend request cancellation attempted with null data");
                return OperationResult<FriendshipUpdateResponseDto>.Failure("Friendship data is required");
            }

            // Override status to cancelled

            _logger.LogInformation("Processing friend request cancellation for FriendshipId: {FriendshipId}, UserId: {UserId}",
                dto.FriendshipId, dto.UserId);

            var validationResult = await FriendshipValidation.ValidateFriendshipStatusUpdateAsync(dto, _userRepository, _friendshipRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Friend request cancellation validation failed for FriendshipId {FriendshipId}, UserId {UserId}: {Error}",
                    dto.FriendshipId, dto.UserId, validationResult.Message);
                return OperationResult<FriendshipUpdateResponseDto>.Failure(validationResult.Message);
            }

            var updateResult = await _friendshipRepository.UpdateFriendAsync(dto);

            if (!updateResult.IsSuccess)
            {
                _logger.LogWarning("Friend request cancellation failed for FriendshipId {FriendshipId}, UserId {UserId}: {Message}",
                    dto.FriendshipId, dto.UserId, updateResult.Message);
                return OperationResult<FriendshipUpdateResponseDto>.Failure(updateResult.Message);
            }

            _logger.LogInformation("Friend request cancelled successfully - FriendshipId: {FriendshipId}, UserId: {UserId}",
                dto.FriendshipId, dto.UserId);

            return OperationResult<FriendshipUpdateResponseDto>.Success(updateResult.Data!, updateResult.Message);
        }

        public async Task<OperationResult<FriendshipUpdateResponseDto>> BlockUserAsync(FriendshipUpdateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("User blocking attempted with null data");
                return OperationResult<FriendshipUpdateResponseDto>.Failure("Friendship data is required");
            }

            // Override status to blocked

            _logger.LogInformation("Processing user blocking for FriendshipId: {FriendshipId}, UserId: {UserId}",
                dto.FriendshipId, dto.UserId);

            var validationResult = await FriendshipValidation.ValidateFriendshipStatusUpdateAsync(dto, _userRepository, _friendshipRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("User blocking validation failed for FriendshipId {FriendshipId}, UserId {UserId}: {Error}",
                    dto.FriendshipId, dto.UserId, validationResult.Message);
                return OperationResult<FriendshipUpdateResponseDto>.Failure(validationResult.Message);
            }

            var updateResult = await _friendshipRepository.UpdateFriendAsync(dto);

            if (!updateResult.IsSuccess)
            {
                _logger.LogWarning("User blocking failed for FriendshipId {FriendshipId}, UserId {UserId}: {Message}",
                    dto.FriendshipId, dto.UserId, updateResult.Message);
                return OperationResult<FriendshipUpdateResponseDto>.Failure(updateResult.Message);
            }

            _logger.LogInformation("User blocked successfully - FriendshipId: {FriendshipId}, UserId: {UserId}",
                dto.FriendshipId, dto.UserId);

            return OperationResult<FriendshipUpdateResponseDto>.Success(updateResult.Data!, updateResult.Message);
        }
        public async Task<OperationResult<FriendshipExistenceResponseDto>> IsFriendshipExistedByIDAsync(FriendshipExistenceRequestDto dto)
        {
            if (dto.UserId1 <= 0)
            {
                _logger.LogWarning("Friendship existence check attempted with invalid UserId1: {UserId1}", dto.UserId1);
                return OperationResult<FriendshipExistenceResponseDto>.Failure("UserId1 must be a positive integer");
            }

            if (dto.UserId2 <= 0)
            {
                _logger.LogWarning("Friendship existence check attempted with invalid UserId2: {UserId2}", dto.UserId2);
                return OperationResult<FriendshipExistenceResponseDto>.Failure("UserId2 must be a positive integer");
            }

            _logger.LogInformation("Processing friendship existence check for UserId1: {UserId1}, UserId2: {UserId2}", dto.UserId1, dto.UserId2);

            var existenceResult = await _friendshipRepository.IsFriendshipExistedByID(dto);

            if (!existenceResult.IsSuccess)
            {
                _logger.LogWarning("Friendship existence check failed for UserId1 {UserId1}, UserId2 {UserId2}: {Message}",
                    dto.UserId1, dto.UserId2, existenceResult.Message);
                return OperationResult<FriendshipExistenceResponseDto>.Failure(existenceResult.Message);
            }

            _logger.LogInformation("Friendship existence check completed successfully - UserId1: {UserId1}, UserId2: {UserId2}, Exists: {FriendshipExists}, Status: {FriendshipStatus}",
                dto.UserId1, dto.UserId2, existenceResult.Data?.FriendshipExists, existenceResult.Data?.FriendshipStatus);

            return OperationResult<FriendshipExistenceResponseDto>.Success(existenceResult.Data!, existenceResult.Message);
        }
   public async Task<OperationResult<GetFriendRequestsResponseDto>> GetFriendRequestsAsync(GetFriendRequestsRequestDto dto)
{
    if (dto == null)
    {
        _logger.LogWarning("Get friend requests attempted with null data");
        return OperationResult<GetFriendRequestsResponseDto>.Failure("Request data is required");
    }

    _logger.LogInformation("Processing get friend requests for UserId: {UserId}", dto.UserId);

    var validationResult = await FriendshipValidation.ValidateGetFriendRequestsAsync(dto, _logger);
    if (!validationResult.IsSuccess)
    {
        _logger.LogWarning("Get friend requests validation failed for UserId {UserId}: {Error}",
            dto.UserId, validationResult.Message);
        return OperationResult<GetFriendRequestsResponseDto>.Failure(validationResult.Message);
    }

    var result = await _friendshipRepository.GetFriendRequestsAsync(dto);

    if (!result.IsSuccess)
    {
        _logger.LogWarning("Get friend requests failed for UserId {UserId}: {Message}",
            dto.UserId, result.Message);
        return OperationResult<GetFriendRequestsResponseDto>.Failure(result.Message);
    }

    _logger.LogInformation("Friend requests retrieved successfully - UserId: {UserId}, Count: {Count}",
        dto.UserId, result.Data?.FriendRequests?.Count ?? 0);

    return OperationResult<GetFriendRequestsResponseDto>.Success(result.Data!, result.Message);
}

public async Task<OperationResult<GetUserFriendsResponseDto>> GetUserFriendsAsync(GetUserFriendsRequestDto dto)
{
    if (dto == null)
    {
        _logger.LogWarning("Get user friends attempted with null data");
        return OperationResult<GetUserFriendsResponseDto>.Failure("Request data is required");
    }

    _logger.LogInformation("Processing get user friends for UserId: {UserId}", dto.UserId);

    var validationResult = await FriendshipValidation.ValidateGetUserFriendsAsync(dto, _logger);
    if (!validationResult.IsSuccess)
    {
        _logger.LogWarning("Get user friends validation failed for UserId {UserId}: {Error}",
            dto.UserId, validationResult.Message);
        return OperationResult<GetUserFriendsResponseDto>.Failure(validationResult.Message);
    }

    var result = await _friendshipRepository.GetUserFriendsAsync(dto);

    if (!result.IsSuccess)
    {
        _logger.LogWarning("Get user friends failed for UserId {UserId}: {Message}",
            dto.UserId, result.Message);
        return OperationResult<GetUserFriendsResponseDto>.Failure(result.Message);
    }

    _logger.LogInformation("User friends retrieved successfully - UserId: {UserId}, Count: {Count}",
        dto.UserId, result.Data?.Friends?.Count ?? 0);

    return OperationResult<GetUserFriendsResponseDto>.Success(result.Data!, result.Message);
}

public async Task<OperationResult<SearchUserFriendsResponseDto>> SearchUserFriendsAsync(SearchUserFriendsRequestDto dto)
{
    if (dto == null)
    {
        _logger.LogWarning("Search user friends attempted with null data");
        return OperationResult<SearchUserFriendsResponseDto>.Failure("Request data is required");
    }

    _logger.LogInformation("Processing search user friends for UserId: {UserId}, Filter: {Filter}",
        dto.UserId, dto.Filter);

    var validationResult = await FriendshipValidation.ValidateSearchUserFriendsAsync(dto, _logger);
    if (!validationResult.IsSuccess)
    {
        _logger.LogWarning("Search user friends validation failed for UserId {UserId}, Filter {Filter}: {Error}",
            dto.UserId, dto.Filter, validationResult.Message);
        return OperationResult<SearchUserFriendsResponseDto>.Failure(validationResult.Message);
    }

    var result = await _friendshipRepository.SearchUserFriendsAsync(dto);

    if (!result.IsSuccess)
    {
        _logger.LogWarning("Search user friends failed for UserId {UserId}: {Message}",
            dto.UserId, result.Message);
        return OperationResult<SearchUserFriendsResponseDto>.Failure(result.Message);
    }

    _logger.LogInformation("User friends search completed successfully - UserId: {UserId}, Count: {Count}",
        dto.UserId, result.Data?.Friends?.Count ?? 0);

    return OperationResult<SearchUserFriendsResponseDto>.Success(result.Data!, result.Message);
}

    }
}