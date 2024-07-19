using AuthApi.Domain.Dto.Auth;
using AuthApi.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.AppStart
{
    public static class SeedData
    {





        internal static async void Seed(WebApplication app)
        {

            var usuarios = new List<RegistrationRequestDto>()
            {
                new RegistrationRequestDto()
                {
                    Email = "fernando@nomellini.net",
                    Password = "Stela137!",
                    FullName = "Fernando Nomelini",
                    Role = "system-admin"
                },
                new RegistrationRequestDto()
                {
                    Email = "stela@nomellini.net",
                    Password = "Stela137!",
                    FullName = "Stela",
                    Role = "tenant-user"
                },
                new RegistrationRequestDto()
                {
                    Email = "tania@nomellini.net",
                    Password = "Stela137!",
                    FullName = "Tania",
                    Role = "tenant-admin"
                }


            };

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _authService = services.GetRequiredService<IAuthService>();

                foreach (var usuario in usuarios)
                {
                    await _authService.Register(usuario);
                    await _authService.AssignRole(usuario.Email!, usuario.Role!);
                }


            }
        }
    }
}
