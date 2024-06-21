using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DtoModels.Models.Authorization
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [DisplayName("Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
