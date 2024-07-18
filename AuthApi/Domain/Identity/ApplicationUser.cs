using Microsoft.AspNetCore.Identity;

namespace AuthApi.Domain.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public string? FullName { get; set; } = default!;

    }
}
