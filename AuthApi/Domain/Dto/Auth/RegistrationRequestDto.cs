namespace AuthApi.Domain.Dto.Auth
{
    public class RegistrationRequestDto
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; } = "";
        public string? Role { get; set; }
    }
}
