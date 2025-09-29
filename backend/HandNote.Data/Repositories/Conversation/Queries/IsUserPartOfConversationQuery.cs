using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Conversation;
using HandNote.Data.DTOs.Conversation.Query;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Conversation.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Conversation.Queries
{
    public class IsUserPartOfConversationQuery
    {
        private const string IsUserPartOfConversationSql = @"
            EXEC [dbo].[sp_IsUserPartOfConversation] 
                @ConversationId, @UserId";

        public static async Task<OperationResult<ConversationExistenceResponseDto>> ExecuteAsync(
            ConversationExistenceRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("IsUserPartOfConversationQuery received null data");
                return OperationResult<ConversationExistenceResponseDto>.Failure("Conversation existence data is required");
            }

            logger.LogInformation("Executing user conversation access check for ConversationId: {ConversationId}, UserId: {UserId}",
                dto.ConversationId, dto.UserId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.ConversationId, dto.UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during user conversation access check for ConversationId {ConversationId}, UserId {UserId}",
                    dto.ConversationId, dto.UserId);
                return OperationResult<ConversationExistenceResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during user conversation access check for ConversationId {ConversationId}, UserId {UserId}. Error: {Error}",
                    dto.ConversationId, dto.UserId, ex.Message);
                return OperationResult<ConversationExistenceResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during user conversation access check for ConversationId {ConversationId}, UserId {UserId}",
                    dto.ConversationId, dto.UserId);
                return OperationResult<ConversationExistenceResponseDto>.Failure("User conversation access check failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, ConversationExistenceRequestDto dto)
        {
            var command = new SqlCommand(IsUserPartOfConversationSql, connection);
            command.AddParameter("@ConversationId", dto.ConversationId);
            command.AddParameter("@UserId", dto.UserId);
            return command;
        }

        private static async Task<OperationResult<ConversationExistenceResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int conversationId,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from user conversation access check procedure for ConversationId {ConversationId}, UserId {UserId}",
                    conversationId, userId);
                return OperationResult<ConversationExistenceResponseDto>.Failure("User conversation access check procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "User conversation access check completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("User conversation access check failed for ConversationId {ConversationId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    conversationId, userId, message, errorNumber);
                return OperationResult<ConversationExistenceResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = ConversationMapper.MapExistenceResponseFromReader(reader);

                    logger.LogInformation("User conversation access check completed successfully - ConversationId: {ConversationId}, UserId: {UserId}, ConversationExists: {ConversationExists}, IsUserPart: {IsUserPartOfConversation}",
                        conversationId, userId, responseDto.ConversationExists, responseDto.IsUserPartOfConversation);

                    return OperationResult<ConversationExistenceResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping user conversation access check result for ConversationId {ConversationId}, UserId {UserId}",
                        conversationId, userId);
                    return OperationResult<ConversationExistenceResponseDto>.Failure("Failed to process user conversation access check result");
                }
            }
        }


    }
}