using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using NLog;
using NLog.Web;
using RestaurantApi.Entities;
using RestaurantApi.HelpersAndExtensions;
using RestaurantApi.Mapper;
using RestaurantApi.Middleware;
using RestaurantApi.Models;
using RestaurantApi.Services;
using RestaurantApi.Services.Interfaces;
using RestaurantApi.Validators;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromAppSettings();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddAutoMapper(typeof(RestaurantMappingProfile).Assembly);
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<CreateRestaurantDto>, CreateRestaurantDtoValidator>();
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

app.SeedDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();