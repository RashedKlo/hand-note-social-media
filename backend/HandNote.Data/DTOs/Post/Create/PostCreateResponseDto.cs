using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Media_Files;
using HandNote.Data.Models;
namespace HandNote.Data.DTOs.Post.Create
{

    public class PostCreateResponseDto
    {

        public Models.Post? Post { get; set; }

        // Media Information
        public List<MediaFileDto?> MediaFiles { get; set; } = [];

        public int MediaCount { get; set; } = 0;



    }

}