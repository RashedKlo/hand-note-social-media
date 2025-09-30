using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.UserSession.Logout
{
     public class LogoutUserRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(320, ErrorMessage = "Email must not exceed 320 characters.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "SessionId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "SessionId must be a positive integer.")]
        public int SessionId { get; set; }
    }
}