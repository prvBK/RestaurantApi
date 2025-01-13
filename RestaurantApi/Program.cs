using NLog;
using NLog.Web;
using RestaurantApi;
using RestaurantApi.Entities;
using RestaurantApi.Mapper;
using RestaurantApi.Middleware;
using RestaurantApi.Services;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromAppSettings();

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddAutoMapper(typeof(RestaurantMappingProfile).Assembly);
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishServive, DishServive>();
builder.Services.AddOpenApi();

// U¿yj domyœlnego logowania
builder.Logging.ClearProviders(); // Opcjonalnie, aby wyczyœciæ inne dostawców logów
builder.Host.UseNLog(); // U¿yj NLog jako loggera

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // https://localhost:7284/scalar/v1
    app.MapScalarApiReference(options =>
    {
        options
        .WithTitle("Restaurant API")
        .WithTheme(ScalarTheme.Mars)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithDarkMode(true);
    });
}

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