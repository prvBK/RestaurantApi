using RestaurantApi.Entities;

namespace RestaurantApi.Seeders
{
    public class DatabaseInitializer
    {
        protected DatabaseInitializer() { }
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            RestaurantDbContext dbContext = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
            new RestaurantSeeder(dbContext).Seed();
        }
    }
}