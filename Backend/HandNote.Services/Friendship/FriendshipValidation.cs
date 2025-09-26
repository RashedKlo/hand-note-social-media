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
        IFriendshipRepository friendshipRepository,
        ILogger logger)
    {
        // Check for self-friendship
        if (dto.RequesterId == dto.AddresseeId)
        {
            return OperationResult<bool>.Failure("Cannot send friend request to yourself");
        }

        // Check if friendship already exists
        var friendshipExistenceResult = await friendshipRepository.IsFriendshipExistedByID(new FriendshipExistenceRequestDto { UserId1 = dto.AddresseeId, UserId2 = dto.RequesterId });
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

    public static OperationResult<bool> ValidateFriendshipStatusUpdateAsync(
         FriendshipUpdateRequestDto dto,
         IFriendshipRepository friendshipRepository,
         ILogger logger)
    {
        // Check for self-friendship
        if (dto.FriendshipId == dto.UserId)
        {
            return OperationResult<bool>.Failure("Cannot send friend request to yourself");
        }

        logger.LogDebug("Friend request creation validation passed for UserId: {UserId}, FriendShipId: {FriendShipId}",
            dto.UserId, dto.FriendshipId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }

}