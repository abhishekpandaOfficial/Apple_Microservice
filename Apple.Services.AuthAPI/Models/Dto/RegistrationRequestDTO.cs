namespace Apple.Services.AuthAPI.Models.Dto
{
    public class RegistrationRequestDTO
    {
        public string Email { get; set; }
        public string NameOfUser { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public DateTime CreatedUser { get; set; }
    }
}
