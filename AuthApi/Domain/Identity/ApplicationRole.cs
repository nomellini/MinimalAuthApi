using Microsoft.AspNetCore.Identity;

namespace AuthApi.Domain.Identity
{
    public class ApplicationRole: IdentityRole<int>
    {
        public List<ApplicationUserRole>? UserRoles { get; set; }
    }
}
