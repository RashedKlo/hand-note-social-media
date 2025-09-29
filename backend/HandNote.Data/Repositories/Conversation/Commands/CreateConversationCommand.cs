using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Conversation.Create;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Conversation.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Conversation.Commands
{
    public class CreateConversationCommand
    {
        private const string CreateConversationSql = @"
            EXEC [dbo].[sp_GetOrCreateConversation] 
                @CurrentUserId, @FriendId";

        public static async Task<OperationResult<ConversationCreateResponseDto>> ExecuteAsync(
            ConversationCreateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("CreateConversationCommand received null conversation data");
                return OperationResult<ConversationCreateResponseDto>.Failure("Conversation data is required");
            }

            logger.LogInformation("Executing conversation creation for CurrentUserId: {CurrentUserId}, FriendId: {FriendId}",
                dto.CurrentUserId, dto.FriendId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.CurrentUserId, dto.FriendId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during conversation creation for CurrentUserId {CurrentUserId}, FriendId {FriendId}",
                    dto.CurrentUserId, dto.FriendId);
                return OperationResult<ConversationCreateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during conversation creation for CurrentUserId {CurrentUserId}, FriendId {FriendId}. Error: {Error}",
                    dto.CurrentUserId, dto.FriendId, ex.Message);
                return OperationResult<ConversationCreateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during conversation creation for CurrentUserId {CurrentUserId}, FriendId {FriendId}",
                    dto.CurrentUserId, dto.FriendId);
                return OperationResult<ConversationCreateResponseDto>.Failure("Conversation creation failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, ConversationCreateRequestDto dto)
        {
            var command = new SqlCommand(CreateConversationSql, connection);
            command.AddParameter("@CurrentUserId", dto.CurrentUserId);
            command.AddParameter("@FriendId", dto.FriendId);
            return command;
        }

        private static async Task<OperationResult<ConversationCreateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int currentUserId,
            int friendId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from conversation creation procedure for CurrentUserId {CurrentUserId}, FriendId {FriendId}",
                    currentUserId, friendId);
                return OperationResult<ConversationCreateResponseDto>.Failure("Conversation creation procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Conversation creation completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Conversation creation failed for CurrentUserId {CurrentUserId}, FriendId {FriendId}: {Message} (Error: {ErrorNumber})",
                    currentUserId, friendId, message, errorNumber);
                return OperationResult<ConversationCreateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = ConversationMapper.MapCreateResponseFromReader(reader);

                    logger.LogInformation("Conversation created/retrieved successfully - ConversationId: {ConversationId}, CurrentUserId: {CurrentUserId}, FriendId: {FriendId}, Friend: {FriendUsername}",
                        responseDto.ConversationId, currentUserId, friendId, responseDto.FriendUsername);

                    return OperationResult<ConversationCreateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping conversation creation result for CurrentUserId {CurrentUserId}, FriendId {FriendId}",
                        currentUserId, friendId);
                    return OperationResult<ConversationCreateResponseDto>.Failure("Failed to process conversation creation result");
                }
            }
        }


    }
}