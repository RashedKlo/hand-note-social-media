using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Media_Files;
using HandNote.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Repositories.Media_Files.Helpers
{
    public class MediaFilesMapper
    {
        public static async Task<MediaFilesCreateResponseDto> MapCreateResponseFromReader(SqlDataReader reader)
        {


            MediaFilesCreateResponseDto response = new MediaFilesCreateResponseDto
            {
                UserId = 0,
                RecordsUpdated = 0,
                TotalMediaFiles = 0,
                UpdatedAt = DateTime.MinValue,
                MediaFilesID = new List<int>(),
            };
            var mediaFileIds = new List<int>();

            // First result set - status information
            if (reader.HasRows && await reader.ReadAsync())
            {
                response = new MediaFilesCreateResponseDto
                {
                    UserId = reader.GetValue<int>("UserId"),
                    RecordsUpdated = reader.GetValue<int>("RecordsInserted"),
                    TotalMediaFiles = reader.GetValue<int>("TotalFilePaths"),
                    UpdatedAt = reader.GetValue<DateTime>("CreatedAt"),
                    MediaFilesID = new List<int>(),
                };
            }

            while (await reader.ReadAsync())
            {
                var mediaFileId = reader.GetValueOrDefault<int>("MediaFileId");
                if (mediaFileId > 0)
                {
                    mediaFileIds.Add(mediaFileId);
                }
            }
            response.MediaFilesID = mediaFileIds;
            return response;
        }
    }
}