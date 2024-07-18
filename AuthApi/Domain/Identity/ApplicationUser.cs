using Microsoft.AspNetCore.Identity;

namespace AuthApi.Domain.Identity
{
    public class ApplicationUser: IdentityUser<string>
    {
        public string? FullName { get; set; } = default!;

        public List<ApplicationUserRole>? UserRoles { get; set; }
    }
}
