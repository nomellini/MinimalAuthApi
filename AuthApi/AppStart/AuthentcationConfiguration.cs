using AuthApi.Domain.Identity;
using AuthApi.Domain.Models;
using AuthApi.Helpers;
using AuthApi.Interfaces;
using AuthApi.Repository;
using AuthApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthApi.AppStart
{
    public static class AuthentcationConfiguration
    {

        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>();


            builder.Services
                .AddAuthentication(op =>
                {
                    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var section = builder.Configuration.GetSection("ApiSettings:JwtOptions:Secret");
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
