using System.ComponentModel.DataAnnotations;

namespace hospital_api.Models
{
    public class RegisterModel
    {
        [Required]
        public string? Username { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
