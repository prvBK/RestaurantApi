using RestaurantApi.Entities;

namespace RestaurantApi.Seeders
{
    public class RestaurantSeeder(RestaurantDbContext _dbContext)
    {
        private static List<Restaurant> Restaurants
        {
            get
            {
                List<Restaurant> restaurants =
                [
                new ()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                         "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                ContactNumber = "33333",
                HasDelivery = true,
                Dishes =
                    [
                        new ()
                        {
                            Name = "Nashville Hot Chicken",
                            Price = 10.30M,
                        },

                        new ()
                        {
                            Name = "Chicken Nuggets",
                            Price = 5.30M,
                        },
                    ],
                Address = new Address()
                {
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                }
            },
                new Restaurant()
                {
                    Name = "McDonald Szewska",
                    Category = "Fast Food",
                    Description =
                        "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    ContactNumber = "222222",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Szewska 2",
                        PostalCode = "30-001"
                    }
                }
                ];

                return restaurants;
            }
        }

        public List<Role> Roles
        {
            get
            {
                return
            [
                new Role {Name = "User" },
                new Role {Name = "Manager" },
                new Role { Name = "Admin" }
            ];
            }
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    List<Role> roles = Roles;
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Restaurants.Any())
                {
                    List<Restaurant> restaurants = Restaurants;
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}