using AuthApi.Domain.Dto.Auth;

namespace AuthApi.Interfaces
{
    public interface IUserService
    {
        Task<List<ApplicationUserDto>> GetUsers(bool addRole = false);
    }
}
