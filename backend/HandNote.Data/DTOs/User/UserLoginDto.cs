using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(320, ErrorMessage = "Email cannot exceed 320 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;


        // Password field for user input.
        // The hash is generated in the backend, so this property holds the plain text password.
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } = string.Empty;
    }
}