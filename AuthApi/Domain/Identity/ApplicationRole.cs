using Microsoft.AspNetCore.Identity;

namespace AuthApi.Domain.Identity
{
    public class ApplicationRole: IdentityRole<string>
    {
        public List<ApplicationUserRole>? UserRoles { get; set; }
    }
}
