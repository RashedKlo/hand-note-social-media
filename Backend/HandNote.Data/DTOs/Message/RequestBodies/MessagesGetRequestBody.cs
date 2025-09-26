using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Message.RequestBodies
{
    public class MessagesGetRequestBody
    {
        [Required(ErrorMessage = "CurrentUserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CurrentUserId must be a positive integer.")]
        public int CurrentUserId { get; set; }
    }
}