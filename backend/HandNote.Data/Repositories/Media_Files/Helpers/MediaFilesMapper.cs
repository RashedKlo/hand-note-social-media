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
        public static MediaFilesCreateResponseDto MapCreateResponseFromReader(SqlDataReader reader)
        {
            return new MediaFilesCreateResponseDto
            {
                UserId = reader.GetValueOrDefault<int>("UserId"),
                RecordsInserted = reader.GetValueOrDefault<int>("RecordsInserted"),
                TotalFilePaths = reader.GetValueOrDefault<int>("TotalFilePaths"),
                CreatedAt = reader.GetValueOrDefault<DateTime>("CreatedAt"),
            };
        }
    }
}