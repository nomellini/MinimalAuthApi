using Microsoft.AspNetCore.Identity;

namespace AuthApi.Domain.Identity
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string? FullName { get; set; } = default!;

        public List<ApplicationUserRole>? UserRoles { get; set; }
    }
}
