using System.ComponentModel.DataAnnotations;

namespace SnapMob_Backend.DTO.AuthDTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[A-Z][a-zA-Z\s]*$",
         ErrorMessage = "Name must start with a capital letter and contain only letters and spaces")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email cannot contain spaces and must be a valid format")]

        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$",
        ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 number, and 1 special character")]
        public string Password { get; set; } = null!;


    }
}
