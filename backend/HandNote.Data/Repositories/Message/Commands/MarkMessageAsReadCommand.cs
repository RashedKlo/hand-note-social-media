using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Message.Add;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Message.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Message.Commands
{
    public class MarkMessageAsReadCommand
    {
        private const string MarkMessagesAsReadSql = @"
            EXEC [dbo].[sp_MarkMessagesAsRead] 
                @ConversationId, @UserId";

        public static async Task<OperationResult<MessagesReadResponseDto>> ExecuteAsync(
            MessagesReadRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("MarkMessageAsReadCommand received null message data");
                return OperationResult<MessagesReadResponseDto>.Failure("Message data is required");
            }

            logger.LogInformation("Executing mark messages as read for ConversationId: {ConversationId}, UserId: {UserId}",
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
                logger.LogError(ex, "Database connection failed during mark messages as read for ConversationId {ConversationId}, UserId {UserId}",
                    dto.ConversationId, dto.UserId);
                return OperationResult<MessagesReadResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during mark messages as read for ConversationId {ConversationId}, UserId {UserId}. Error: {Error}",
                    dto.ConversationId, dto.UserId, ex.Message);
                return OperationResult<MessagesReadResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during mark messages as read for ConversationId {ConversationId}, UserId {UserId}",
                    dto.ConversationId, dto.UserId);
                return OperationResult<MessagesReadResponseDto>.Failure("Mark messages as read failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, MessagesReadRequestDto dto)
        {
            var command = new SqlCommand(MarkMessagesAsReadSql, connection);
            command.AddParameter("@ConversationId", dto.ConversationId);
            command.AddParameter("@UserId", dto.UserId);
            return command;
        }

        private static async Task<OperationResult<MessagesReadResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int conversationId,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from mark messages as read procedure for ConversationId {ConversationId}, UserId {UserId}",
                    conversationId, userId);
                return OperationResult<MessagesReadResponseDto>.Failure("Mark messages as read procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Mark messages as read completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Mark messages as read failed for ConversationId {ConversationId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    conversationId, userId, message, errorNumber);
                return OperationResult<MessagesReadResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = MessageMapper.MapReadResponseFromReader(reader);

                    logger.LogInformation("Messages marked as read successfully - ConversationId: {ConversationId}, UserId: {UserId}, Messages Marked: {MessagesMarkedAsRead}",
                        conversationId, userId, responseDto.MessagesMarkedAsRead);
                    return OperationResult<MessagesReadResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping mark messages as read result for ConversationId {ConversationId}, UserId {UserId}",
                        conversationId, userId);
                    return OperationResult<MessagesReadResponseDto>.Failure("Failed to process mark messages as read result");
                }
            }
        }

    }
}