using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using HandNote.Data.Settings;
using HandNote.Data.DTOs.Media_Files;
using HandNote.Data.Results;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Media_Files.Helpers;

namespace HandNote.Data.Repositories.Media_Files.Commands
{
    public static class CreateMediaFilesCommand
    {
        private const string CreateMediaFilesSql = @"
            EXEC [dbo].[sp_CreateMediaFiles] 
                @UserID, @FilePaths";

        public static async Task<OperationResult<MediaFilesCreateResponseDto>> ExecuteAsync(
            MediaFilesCreateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("CreateMediaFilesCommand received null request data");
                return OperationResult<MediaFilesCreateResponseDto>.Failure("Request data is required");
            }

            logger.LogInformation("Executing media files creation for UserId: {UserId}, FileCount: {FileCount}",
                dto.UserId, dto.FilePaths.Count);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53) // Connection timeout or network issues
            {
                logger.LogError(ex, "Database connection failed during media files creation for UserId: {UserId}", dto.UserId);
                return OperationResult<MediaFilesCreateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during media files creation for UserId: {UserId}. Error: {Error}",
                    dto.UserId, ex.Message);
                return OperationResult<MediaFilesCreateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during media files creation for UserId: {UserId}", dto.UserId);
                return OperationResult<MediaFilesCreateResponseDto>.Failure("Media files creation failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, MediaFilesCreateRequestDto dto)
        {
            var command = new SqlCommand(CreateMediaFilesSql, connection);

            // Add UserId parameter
            command.AddParameter("@UserID", dto.UserId);

            // Create DataTable for file paths
            var filePathsTable = new DataTable();
            filePathsTable.Columns.Add("file_path", typeof(string));

            // Populate DataTable with file paths
            foreach (var filePath in dto.FilePaths)
            {
                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    filePathsTable.Rows.Add(filePath.Trim());
                }
            }

            // Add table-valued parameter
            var tableParameter = command.Parameters.AddWithValue("@FilePaths", filePathsTable);
            tableParameter.SqlDbType = SqlDbType.Structured;
            tableParameter.TypeName = "dbo.FilePathsTableType";

            return command;
        }

        private static async Task<OperationResult<MediaFilesCreateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from media files creation procedure for UserId: {UserId}", userId);
                return OperationResult<MediaFilesCreateResponseDto>.Failure("Media files creation procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status");
            var message = reader.GetValueOrDefault<string>("Message") ?? "Media files creation completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Media files creation failed for UserId: {UserId}: {Message} (Error: {ErrorNumber})",
                    userId, message, errorNumber);
                return OperationResult<MediaFilesCreateResponseDto>.Failure(message);
            }
            else
            {
                var response = await MediaFilesMapper.MapCreateResponseFromReader(reader);

                logger.LogInformation("Media files created successfully - UserId: {UserId}, RecordsInserted: {RecordsInserted}",
                    response.UserId, response.RecordsUpdated);

                return OperationResult<MediaFilesCreateResponseDto>.Success(response, "Media files created successfully");
            }
        }
    }
}