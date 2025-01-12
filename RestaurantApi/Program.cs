using NLog;
using NLog.Web;
using RestaurantApi;
using RestaurantApi.Entities;
using RestaurantApi.Mapper;
using RestaurantApi.Middleware;
using RestaurantApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromAppSettings();

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddAutoMapper(typeof(RestaurantMappingProfile).Assembly);
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

// U�yj domy�lnego logowania
builder.Logging.ClearProviders(); // Opcjonalnie, aby wyczy�ci� inne dostawc�w log�w
builder.Host.UseNLog(); // U�yj NLog jako loggera

WebApplication app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

using (IServiceScope scope = app.Services.CreateScope())
{
    RestaurantDbContext dbContext = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
    new RestaurantSeeder(dbContext).Seed();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();