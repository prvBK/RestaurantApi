using RestaurantApi.Entities;
using RestaurantApi.Seeders;

public static class ApplicationExtensions
{
    public static void SeedDatabase(this WebApplication app)
    {
        using (IServiceScope scope = app.Services.CreateScope())
        {
            RestaurantDbContext dbContext = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
            new RestaurantSeeder(dbContext).Seed();
        }
    }
}
