using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Message.Get;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Message.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Message.Queries
{
    public class GetMessagesQuery
    {
        private const string GetMessagesSql = @"
            EXEC [dbo].[sp_GetMessages] 
                @ConversationId, @CurrentUserId, @PageSize, @PageNumber";

        public static async Task<OperationResult<List<MessagesGetResponseDto>>> ExecuteAsync(
            MessagesGetRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("GetMessagesQuery received null message data");
                return OperationResult<List<MessagesGetResponseDto>>.Failure("Message data is required");
            }

            logger.LogInformation("Executing get messages for ConversationId: {ConversationId}, CurrentUserId: {CurrentUserId}, PageSize: {PageSize}, PageNumber: {PageNumber}",
                dto.ConversationId, dto.CurrentUserId, dto.PageSize, dto.PageNumber);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.ConversationId, dto.CurrentUserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during get messages for ConversationId {ConversationId}, CurrentUserId {CurrentUserId}",
                    dto.ConversationId, dto.CurrentUserId);
                return OperationResult<List<MessagesGetResponseDto>>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during get messages for ConversationId {ConversationId}, CurrentUserId {CurrentUserId}. Error: {Error}",
                    dto.ConversationId, dto.CurrentUserId, ex.Message);
                return OperationResult<List<MessagesGetResponseDto>>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during get messages for ConversationId {ConversationId}, CurrentUserId {CurrentUserId}",
                    dto.ConversationId, dto.CurrentUserId);
                return OperationResult<List<MessagesGetResponseDto>>.Failure("Get messages failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, MessagesGetRequestDto dto)
        {
            var command = new SqlCommand(GetMessagesSql, connection);
            command.AddParameter("@ConversationId", dto.ConversationId);
            command.AddParameter("@CurrentUserId", dto.CurrentUserId);
            command.AddParameter("@PageSize", dto.PageSize);
            command.AddParameter("@PageNumber", dto.PageNumber);
            return command;
        }

        private static async Task<OperationResult<List<MessagesGetResponseDto>>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int conversationId,
            int currentUserId)
        {
            var messages = new List<MessagesGetResponseDto>();

            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from get messages procedure for ConversationId {ConversationId}, CurrentUserId {CurrentUserId}",
                    conversationId, currentUserId);
                return OperationResult<List<MessagesGetResponseDto>>.Failure("Get messages procedure returned no result");
            }

            // Process the first row to check status
            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Get messages completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Get messages failed for ConversationId {ConversationId}, CurrentUserId {CurrentUserId}: {Message} (Error: {ErrorNumber})",
                    conversationId, currentUserId, message, errorNumber);
                return OperationResult<List<MessagesGetResponseDto>>.Failure(message);
            }

            try
            {
                // Process all rows for successful results
                do
                {
                    // Skip rows that don't have message data (error rows)
                    if (reader.GetValueOrDefault<int?>("MessageId").HasValue)
                    {
                        var messageDto = MessageMapper.MapMessageFromReader(reader);
                        messages.Add(messageDto);
                    }
                } while (await reader.ReadAsync());

                logger.LogInformation("Get messages completed successfully - ConversationId: {ConversationId}, CurrentUserId: {CurrentUserId}, Messages Count: {MessageCount}",
                    conversationId, currentUserId, messages.Count);

                return OperationResult<List<MessagesGetResponseDto>>.Success(messages,
                    $"Successfully retrieved {messages.Count} messages");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error mapping get messages result for ConversationId {ConversationId}, CurrentUserId {CurrentUserId}",
                    conversationId, currentUserId);
                return OperationResult<List<MessagesGetResponseDto>>.Failure("Failed to process get messages result");
            }
        }


    }
}