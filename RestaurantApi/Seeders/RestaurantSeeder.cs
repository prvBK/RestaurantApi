using Bogus; // Upewnij się, że dodasz tę bibliotekę do swojego projektu
using RestaurantApi.Entities;

namespace RestaurantApi.Seeders
{
    public class RestaurantSeeder(RestaurantDbContext dbContext)
    {
        public List<Restaurant> GenerateRandomRestaurants(int count)
        {
            List<int> createdByIds = dbContext.Users.Select(u => u.Id).ToList();

            List<string> DishNames = new List<string>
        {
            "Pizza Margherita", "Spaghetti Carbonara", "Sushi", "Tacos", "Burger",
            "Caesar Salad", "Pad Thai", "Ramen", "Fish and Chips", "Steak Frites",
            "Lasagna", "Fried Rice", "Chow Mein", "Chicken Curry", "Beef Stroganoff",
            "Peking Duck", "Grilled Cheese Sandwich", "Quiche Lorraine", "Moussaka",
            "Biryani", "Goulash", "Pasta Primavera", "Seafood Paella",
            "Vegetable Stir Fry", "Pulled Pork Sandwiches", "Falafel Wraps",
            "Kebabs", "Ceviche", "Clam Chowder", "Stuffed Peppers",
            "Crepes Suzette"
        };

            Faker faker = new Faker();
            List<Restaurant> restaurants = new List<Restaurant>();

            for (int i = 0; i < count; i++)
            {
                Restaurant restaurant = new Restaurant
                {
                    Name = faker.Company.CompanyName(),
                    Category = faker.PickRandom(new[] { "Fast Food", "Italian", "Chinese", "Mexican", "Indian" }),
                    Description = faker.Lorem.Sentence(10),
                    ContactEmail = faker.Internet.Email(),
                    ContactNumber = faker.Phone.PhoneNumber(),
                    HasDelivery = faker.Random.Bool(),
                    Address = new Address
                    {
                        City = faker.Address.City(),
                        Street = faker.Address.StreetAddress(),
                        PostalCode = faker.Address.ZipCode()
                    },
                    CreatedById = faker.PickRandom(createdByIds)
                };

                // Dodaj kilka losowych dań do restauracji
                restaurant.Dishes = new List<Dish>
                {
                    new Dish { Name = faker.PickRandom(DishNames), Price = faker.Finance.Amount(5, 30) },
                    new Dish { Name = faker.PickRandom(DishNames), Price = faker.Finance.Amount(5, 30) }
                };

                restaurants.Add(restaurant);
            }

            return restaurants;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.Roles.Any())
                {
                    List<Role> roles = new List<Role>
                    {
                        new Role { Name = "User" },
                        new Role { Name = "Manager" },
                        new Role { Name = "Admin" }
                    };
                    dbContext.Roles.AddRange(roles);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Restaurants.Any())
                {
                    Console.WriteLine("Restaurant start generate" + DateTime.Now);
                    List<Restaurant> restaurants = GenerateRandomRestaurants(20);
                    Console.WriteLine("Restaurant start AddRange" + DateTime.Now);

                    dbContext.Restaurants.AddRange(restaurants);
                    Console.WriteLine("Restaurant start SaveChanges" + DateTime.Now);
                    dbContext.SaveChanges();
                    Console.WriteLine("Restaurant added");
                }
            }
        }
    }
}
