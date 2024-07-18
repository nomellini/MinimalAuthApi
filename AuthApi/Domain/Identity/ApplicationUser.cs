using AuthApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Domain.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public string? FullName { get; set; } = default!;

        public string? TenantId { get; set; }
    }
}
