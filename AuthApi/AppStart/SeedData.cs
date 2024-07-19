using AuthApi.Domain.Identity;
using AuthApi.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.AppStart
{
    public static class SeedData
    {
        internal static async void Seed(WebApplication app)
        {

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _authService = services.GetRequiredService<IAuthService>();

                await _authService.Register(new Domain.Dto.Auth.RegistrationRequestDto()
                {
                    Email = "fernando@nomellini.net",
                    Password = "Stela137!",
                    FullName = "Fernando Nomelini"
                });
                await _authService.AssignRole("fernando@nomellini.net", "system-admin");
            }
        }
    }
}
