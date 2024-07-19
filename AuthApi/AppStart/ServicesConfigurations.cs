using AuthApi.Interfaces;
using AuthApi.Services;

namespace AuthApi.AppStart
{
    public static class ServicesConfigurations
    {
        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITenantService, TenantService>();
        }
    }
}