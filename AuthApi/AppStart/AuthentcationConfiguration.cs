using AuthApi.Domain.Identity;
using AuthApi.Domain.Models;
using AuthApi.Helpers;
using AuthApi.Interfaces;
using AuthApi.Repository;
using AuthApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.MicrosoftExtensions;
using System.Security.Claims;
using System.Text;

namespace AuthApi.AppStart
{
    public static class AuthentcationConfiguration
    {

        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>();


            builder.Services.AddHttpContextAccessor();



            builder.Services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "MultiScheme";
                        options.DefaultChallengeScheme = "MultiScheme";
                    }).AddPolicyScheme("MultiScheme", "Cookie or JWT", options =>
                    {
                        options.ForwardDefaultSelector = context =>
                        {
                            // Verifica se a requisição tem um token JWT
                            if (context.Request.Headers.ContainsKey("Authorization"))
                            {
                                return JwtBearerDefaults.AuthenticationScheme;
                            }

                            // Caso contrário, usa autenticação por cookie
                            return CookieAuthenticationDefaults.AuthenticationScheme;
                        };
                    }).AddCookie(options =>
                    {
                        options.LoginPath = "/Login";
                        options.LogoutPath = "/Logout";
                    }).AddJwtBearer(options =>
                    {
                        var section = builder.Configuration.GetSection("JwtOptions:Secret");
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                                      .GetBytes(section.Value ?? "")),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                );

        }

    }
}
