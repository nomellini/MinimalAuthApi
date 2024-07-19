using AuthApi.Domain.Dto;
using AuthApi.Domain.Dto.Auth;

namespace AuthApi.Interfaces
{
    public interface IAuthService
    {
        Task<String> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto> AssignRole(string email, string roleName);
        Task<List<ApplicationUserDto>> GetUsers();
    }
}
