
using AuthApi.AppStart;
using Microsoft.OpenApi.MicrosoftExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
IdentityConfigurations.ConfigureServices(builder);
AuthentcationConfiguration.ConfigureServices(builder);
ServicesConfigurations.ConfigureServices(builder);
RepositoryConfigurations.ConfigureServices(builder);
SwaggerConfigurations.ConfigureServices(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

SeedData.Seed(app);

app.Run();

