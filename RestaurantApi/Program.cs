using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using RestaurantApi.Authentication;
using RestaurantApi.Authorization;
using RestaurantApi.Entities;
using RestaurantApi.HelpersAndExtensions;
using RestaurantApi.Mapper;
using RestaurantApi.Middleware;
using RestaurantApi.Models;
using RestaurantApi.Services;
using RestaurantApi.Services.Interfaces;
using RestaurantApi.Validators;
using Scalar.AspNetCore;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromAppSettings();

AuthenticationSettings authenticationSettings = new();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey ?? string.Empty))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "Polish", "German"));
    options.AddPolicy("Atleast20", builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
    options.AddPolicy("MinumumNResaurantCreate", builder => builder.AddRequirements(new MinimumNRestaurantCreated(2)));
});

builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirmentHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MinimumNRestaurantCreatedHandler>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddAutoMapper(typeof(RestaurantMappingProfile).Assembly);
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<CreateRestaurantDto>, CreateRestaurantDtoValidator>();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // https://localhost:7284/scalar/v1
    app.MapScalarApiReference(options =>
    {
        options
        .WithTitle("Scalar API " + authenticationSettings.TokenToTestingDescription ?? string.Empty)
        .WithTheme(ScalarTheme.Mars)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithTestRequestButton(true)
        .WithDarkMode(true);
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<GlobalHeaderMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.SeedDatabase();
app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();