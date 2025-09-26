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
    public class AddMessageCommand
    {
        private const string AddMessageSql = @"
            EXEC [dbo].[sp_AddMessage] 
                @ConversationId, @SenderId, @Content, @MessageType, @MediaId";

        public static async Task<OperationResult<MessageAddResponseDto>> ExecuteAsync(
            MessageAddRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("AddMessageCommand received null message data");
                return OperationResult<MessageAddResponseDto>.Failure("Message data is required");
            }

            logger.LogInformation("Executing add message for ConversationId: {ConversationId}, SenderId: {SenderId}, MessageType: {MessageType}",
                dto.ConversationId, dto.SenderId, dto.MessageType);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.ConversationId, dto.SenderId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during add message for ConversationId {ConversationId}, SenderId {SenderId}",
                    dto.ConversationId, dto.SenderId);
                return OperationResult<MessageAddResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during add message for ConversationId {ConversationId}, SenderId {SenderId}. Error: {Error}",
                    dto.ConversationId, dto.SenderId, ex.Message);
                return OperationResult<MessageAddResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during add message for ConversationId {ConversationId}, SenderId {SenderId}",
                    dto.ConversationId, dto.SenderId);
                return OperationResult<MessageAddResponseDto>.Failure("Add message failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, MessageAddRequestDto dto)
        {
            var command = new SqlCommand(AddMessageSql, connection);
            command.AddParameter("@ConversationId", dto.ConversationId);
            command.AddParameter("@SenderId", dto.SenderId);
            command.AddParameter("@Content", dto.Content);
            command.AddParameter("@MessageType", dto.MessageType);
            command.AddParameter("@MediaId", dto.MediaId.HasValue ? dto.MediaId.Value : (object)DBNull.Value);
            return command;
        }

        private static async Task<OperationResult<MessageAddResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int conversationId,
            int senderId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from add message procedure for ConversationId {ConversationId}, SenderId {SenderId}",
                    conversationId, senderId);
                return OperationResult<MessageAddResponseDto>.Failure("Add message procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Add message completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Add message failed for ConversationId {ConversationId}, SenderId {SenderId}: {Message} (Error: {ErrorNumber})",
                    conversationId, senderId, message, errorNumber);
                return OperationResult<MessageAddResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = MessageMapper.MapMessageCreationFromReader(reader);

                    logger.LogInformation("Message added successfully - MessageId: {MessageId}, ConversationId: {ConversationId}, SenderId: {SenderId}, MessageType: {MessageType}",
                        responseDto.MessageId, conversationId, senderId, responseDto.MessageType);

                    return OperationResult<MessageAddResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping add message result for ConversationId {ConversationId}, SenderId {SenderId}",
                        conversationId, senderId);
                    return OperationResult<MessageAddResponseDto>.Failure("Failed to process add message result");
                }
            }
        }


    }
}