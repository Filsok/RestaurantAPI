using RestaurantsAPI.Entities;

namespace RestaurantsAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDBContext _dbContext;

        public RestaurantSeeder(RestaurantDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Luisvill, Kentucky.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name="Nashville Hot Chicken",
                            Price = 10.30M,
                        },
                        new Dish()
                        {
                            Name="Chicken Nuggets",
                            Price = 5.30M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonalds",
                    Category = "FastFood",
                    Description = "Something about McDonalds.",
                    ContactEmail = "contact@mddonalds.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name="BigMac",
                            Price = 4.00M,
                        },
                        new Dish()
                        {
                            Name="Lumberjack Burger",
                            Price = 5.20M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Niepołomnice",
                        Street = "Główna 15",
                        PostalCode = "xx-005"
                    }
                }
            };
            return restaurants;
        }
    }
}
