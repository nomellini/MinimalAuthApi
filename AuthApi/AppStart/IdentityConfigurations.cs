using AuthApi.Domain.Identity;
using AuthApi.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AuthApi.AppStart
{
    public static class IdentityConfigurations
    {
        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentityCore<ApplicationUser>(op =>
            {
                op.Password.RequireDigit = true;
                op.Password.RequireNonAlphanumeric = true;
                op.Password.RequireLowercase = true;
                op.Password.RequireUppercase = true;
                op.Password.RequiredLength = 8;
            });



        }
    }
}
