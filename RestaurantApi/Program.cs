using RestaurantApi;
using RestaurantApi.Entities;
using RestaurantApi.Mapper;
using RestaurantApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddAutoMapper(typeof(RestaurantMappingProfile).Assembly);
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    RestaurantDbContext dbContext = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
    RestaurantSeeder seeder = new RestaurantSeeder(dbContext); // Przekazywanie dbContext do konstruktora seeda

    try
    {
        seeder.Seed(); // Zak�adaj�c, �e metoda Seed przyjmuje RestaurantDbContext jako parametr
    }
    catch (Exception ex)
    {
        // Loguj wyj�tek (mo�esz u�y� dowolnej biblioteki do logowania)
        Console.WriteLine($"Wyst�pi� b��d podczas inicjalizacji bazy danych: {ex.Message}");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();