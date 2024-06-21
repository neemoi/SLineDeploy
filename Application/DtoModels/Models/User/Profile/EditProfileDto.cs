namespace Application.DtoModels.Models.User.Profile
{
    public class EditProfileDto
    {
        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string? PhoneNumber { get; set; }
        
        public string? Address { get; set; }

        public DateTime? Birthday { get; set; }

        public string? CurrentPassword { get; set; }

        public string? NewPassword { get; set; }
    }
}
