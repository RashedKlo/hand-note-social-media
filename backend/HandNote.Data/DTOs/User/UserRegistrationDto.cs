using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.User;

public class UserRegistrationDto
{

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name can only contain letters and spaces")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last name can only contain letters and spaces")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "City ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "City ID must be a positive number")]
    public int CityId { get; set; }

    // Corresponds to 'username'
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    [RegularExpression("^[a-zA-Z0-9._]+$", ErrorMessage = "Username can only contain letters, numbers, dots, and underscores.")]
    public string UserName { get; set; } = string.Empty;

    // Corresponds to 'email'
    [Required(ErrorMessage = "Email is required.")]
    [StringLength(320, ErrorMessage = "Email cannot exceed 320 characters.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;


    // Password field for user input.
    // The hash is generated in the backend, so this property holds the plain text password.
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; } = string.Empty;

    // Confirmation field to verify the password.
    [Required(ErrorMessage = "Password confirmation is required.")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;



}

