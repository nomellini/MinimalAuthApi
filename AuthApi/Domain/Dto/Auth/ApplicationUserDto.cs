namespace AuthApi.Domain.Dto.Auth
{
    public class ApplicationUserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }

        public IList<string>? Roles { get; set; }
    }
}
