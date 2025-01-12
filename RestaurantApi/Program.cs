using RestaurantApi;
using RestaurantApi.Entities;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    RestaurantDbContext dbContext = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
    RestaurantSeeder seeder = new RestaurantSeeder(dbContext); // Przekazywanie dbContext do konstruktora seeda

    try
    {
        seeder.Seed(); // Zak³adaj¹c, ¿e metoda Seed przyjmuje RestaurantDbContext jako parametr
    }
    catch (Exception ex)
    {
        // Loguj wyj¹tek (mo¿esz u¿yæ dowolnej biblioteki do logowania)
        Console.WriteLine($"Wyst¹pi³ b³¹d podczas inicjalizacji bazy danych: {ex.Message}");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();