using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Media_Files
{
    public class MediaFileDto
    {

        [JsonPropertyName("mediaId")]
        public int MediaId { get; set; }


        [JsonPropertyName("filePath")]
        public string FilePath { get; set; } = string.Empty;

        [JsonPropertyName("displayOrder")]
        public int DisplayOrder { get; set; }

        [JsonPropertyName("uploadedAt")]
        public DateTime? UploadedAt { get; set; }
    }


}