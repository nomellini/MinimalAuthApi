using AuthApi.AppStart;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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
app.Run();