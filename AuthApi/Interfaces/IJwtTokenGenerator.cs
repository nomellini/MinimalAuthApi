using AuthApi.Domain.Identity;

namespace AuthApi.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
