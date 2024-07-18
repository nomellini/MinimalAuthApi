
using AuthApi.Interfaces;
using AuthApi.Repository;

namespace AuthApi.AppStart
{
    public static class RepositoryConfigurations
    {
        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEntityRepository, EntityRepository>();
        }
    }
}
