// FriendshipValidation.cs
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship.Create;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.DTOs.Friendship.Update;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

public static class FriendshipValidation
{
    public static async Task<OperationResult<bool>> ValidateFriendRequestCreationAsync(
        FriendshipCreateRequestDto dto,
        IUserRepository userRepository,
        IFriendshipRepository friendshipRepository,
        ILogger logger)
    {
        // Check for self-friendship
        if (dto.RequesterId == dto.AddresseeId)
        {
            return OperationResult<bool>.Failure("Cannot send friend request to yourself");
        }

        // Check if requester exists
        var requesterExistenceResult = await userRepository.(dto.RequesterId);
        if (!requesterExistenceResult.IsSuccess)
        {
            logger.LogWarning("Failed to check requester existence for RequesterId {RequesterId}: {Message}",
                dto.RequesterId, requesterExistenceResult.Message);
            return OperationResult<bool>.Failure("Unable to verify requester existence");
        }

        if (requesterExistenceResult.Data?.UserExists != true)
        {
            logger.LogWarning("Friend request creation attempted for non-existent RequesterId {RequesterId}", dto.RequesterId);
            return OperationResult<bool>.Failure("Requester not found");
        }


        // Check if friendship already exists
        var friendshipExistenceResult = await friendshipRepository.IsFriendshipExistedByID(dto);
        if (!friendshipExistenceResult.IsSuccess)
        {
            logger.LogWarning("Failed to check friendship existence for RequesterId {RequesterId}, AddresseeId {AddresseeId}: {Message}",
                dto.RequesterId, dto.AddresseeId, friendshipExistenceResult.Message);
            return OperationResult<bool>.Failure("Unable to verify friendship status");
        }

        if (friendshipExistenceResult.Data?.FriendshipExists == true)
        {
            var status = friendshipExistenceResult.Data.FriendshipStatus;
            if (status == "pending")
            {
                logger.LogWarning("Friend request creation attempted when request already pending for RequesterId {RequesterId}, AddresseeId {AddresseeId}",
                    dto.RequesterId, dto.AddresseeId);
                return OperationResult<bool>.Failure("Friend request already exists");
            }
            else if (status == "accepted")
            {
                logger.LogWarning("Friend request creation attempted when users are already friends for RequesterId {RequesterId}, AddresseeId {AddresseeId}",
                    dto.RequesterId, dto.AddresseeId);
                return OperationResult<bool>.Failure("Users are already friends");
            }
            else if (status == "blocked")
            {
                logger.LogWarning("Friend request creation attempted when one user has blocked the other for RequesterId {RequesterId}, AddresseeId {AddresseeId}",
                    dto.RequesterId, dto.AddresseeId);
                return OperationResult<bool>.Failure("Cannot send friend request. One user has blocked the other");
            }
        }

        logger.LogDebug("Friend request creation validation passed for RequesterId: {RequesterId}, AddresseeId: {AddresseeId}",
            dto.RequesterId, dto.AddresseeId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }

    public static async Task<OperationResult<bool>> ValidateFriendshipStatusUpdateAsync(
        FriendshipUpdateRequestDto dto,
        IUserRepository userRepository,
        IFriendshipRepository friendshipRepository,
        ILogger logger)
    {



        // Additional validation would be handled by the stored procedure
        // since it has access to the friendship details and can validate
        // user permissions and status transitions properly

        logger.LogDebug("Friendship status update validation passed for FriendshipId: {FriendshipId}, UserId: {UserId}",
             dto.FriendshipId, dto.UserId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }


    public static Task<OperationResult<bool>> ValidateGetFriendRequestsAsync(
        GetFriendRequestsRequestDto dto,
        ILogger logger)
    {
        if (dto.UserId <= 0)
            return Task.FromResult(OperationResult<bool>.Failure("UserId must be a positive integer"));

        if (dto.Page <= 0)
            return Task.FromResult(OperationResult<bool>.Failure("Page must be a positive integer"));

        if (dto.Limit <= 0 || dto.Limit > 100)
            return Task.FromResult(OperationResult<bool>.Failure("Limit must be between 1 and 100"));

        logger.LogDebug("Friend requests validation passed for UserId {UserId}", dto.UserId);
        return Task.FromResult(OperationResult<bool>.Success(true, "Validation passed"));
    }

    public static Task<OperationResult<bool>> ValidateGetUserFriendsAsync(
        GetUserFriendsRequestDto dto,
        ILogger logger)
    {
        if (dto.UserId <= 0)
            return Task.FromResult(OperationResult<bool>.Failure("UserId must be a positive integer"));

        if (dto.Page <= 0)
            return Task.FromResult(OperationResult<bool>.Failure("Page must be a positive integer"));

        if (dto.Limit <= 0 || dto.Limit > 100)
            return Task.FromResult(OperationResult<bool>.Failure("Limit must be between 1 and 100"));

        logger.LogDebug("Get user friends validation passed for UserId {UserId}", dto.UserId);
        return Task.FromResult(OperationResult<bool>.Success(true, "Validation passed"));
    }

    public static Task<OperationResult<bool>> ValidateSearchUserFriendsAsync(
        SearchUserFriendsRequestDto dto,
        ILogger logger)
    {
        if (dto.UserId <= 0)
            return Task.FromResult(OperationResult<bool>.Failure("UserId must be a positive integer"));

        if (!string.IsNullOrWhiteSpace(dto.Filter) && dto.Filter.Length > 200)
            return Task.FromResult(OperationResult<bool>.Failure("Filter must not exceed 200 characters"));

        if (dto.Page <= 0)
            return Task.FromResult(OperationResult<bool>.Failure("Page must be a positive integer"));

        if (dto.Limit <= 0 || dto.Limit > 100)
            return Task.FromResult(OperationResult<bool>.Failure("Limit must be between 1 and 100"));

        logger.LogDebug("Search user friends validation passed for UserId {UserId}, Filter {Filter}", dto.UserId, dto.Filter);
        return Task.FromResult(OperationResult<bool>.Success(true, "Validation passed"));
    }

}