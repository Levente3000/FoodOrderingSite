using FoodOrderWebApi.Model;
using FoodOrderWebApi.Repository;

namespace FoodOrderWebApi.Configuration
{
    public class DbInitializer
    {
        private static FoodOrderDbContext _context = null!;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<FoodOrderDbContext>();

            if (!_context.Database.EnsureCreated())
            {
                return;
            }

            SeedCategories();
            SeedRestaurants();
            SeedProducts();
            SeedOpeningHours();
        }
        private static void SeedCategories()
        {
            var categories = new Category[]
            {
                new Category
                {
                    Name = "Soup",
                    PictureName = "soup.jpg"
                },
                new Category
                {
                    Name = "Drink",
                    PictureName = "drink.jpg"
                },
                new Category
                {
                    Name = "Pizza",
                    PictureName = "pizza.jpg"
                },
                new Category
                {
                    Name = "Pasta",
                    PictureName = "pasta.jpg"
                },
            };

            foreach (Category c in categories)
            {
                _context.Categories.Add(c);
            }

            _context.SaveChanges();
        }

        private static void SeedRestaurants()
        {
            var restaurants = new Restaurant[]
            {
                new Restaurant
                {
                    Name = "Karen bar",
                    Description = "something",
                    Address = "Budapest, Lágymányosi campus",
                    PhoneNumber = "06300000001",
                    LogoName = "karen_bar.jpg",
                    OpeningHourId = 1,
                    ClosingHourId = 2,
                },
                new Restaurant
                {
                    Name = "Blue Parrot",
                    Description = "soemthing",
                    Address = "Budapest, Wesselényi street",
                    PhoneNumber = "06300000002",
                    LogoName = "blue_parrot.jpg",
                    OpeningHourId = 3,
                    ClosingHourId = 4,
                },
            };

            foreach (Restaurant r in restaurants)
            {
                _context.Restaurants.Add(r);
            }

            _context.SaveChanges();
        }

        private static void SeedProducts()
        {
            var products = new Product[]
            {
                new Product
                {
                    Name = "Goulash Soup",
                    Description = "0,5L soup",
                    Price= 1200,
                    PictureName = "",
                    CategoryName = "Soup",
                    RestaurantId = 1,
                },
                new Product
                {
                    Name = "Margherita",
                    Description = "32cm",
                    Price= 2000,
                    PictureName = "",
                    CategoryName = "Pizza",
                    RestaurantId = 1,
                },
                new Product
                {
                    Name = "BBQ",
                    Description = "32cm",
                    Price= 2300,
                    PictureName = "",
                    CategoryName = "Pizza",
                    RestaurantId = 1,
                },
                new Product
                {
                    Name = "Pesto pasta",
                    Description = "one serving",
                    Price= 1800,
                    PictureName = "",
                    CategoryName = "Pasta",
                    RestaurantId = 2,
                },
                new Product
                {
                    Name = "Bolognese pasta",
                    Description = "one serving",
                    Price= 1900,
                    PictureName = "",
                    CategoryName = "Pasta",
                    RestaurantId = 2,
                },
                new Product
                {
                    Name = "Coca Cola",
                    Description = "0,33L",
                    Price= 600,
                    PictureName = "",
                    CategoryName = "Drink",
                    RestaurantId = 2,
                },
            };

            foreach (Product p in products)
            {
                _context.Products.Add(p);
            }

            _context.SaveChanges();
        }

        private static void SeedOpeningHours()
        {
            var openingHours = new OpeningHour[]
            {
                new OpeningHour
                {
                    RestaurantId = 1,
                    Monday = new DateTime(1,1,1,8,0,0),
                    Tuesday = new DateTime(1,1,1,8,0,0),
                    Wednesday = new DateTime(1,1,1,8,0,0),
                    Thursday = new DateTime(1,1,1,8,0,0),
                    Friday = new DateTime(1,1,1,8,0,0),
                    Saturday = null,
                    Sunday = null,
                },
                new OpeningHour
                {
                    RestaurantId = 1,
                    Monday = new DateTime(1,1,1,18,0,0),
                    Tuesday = new DateTime(1,1,1,20,0,0),
                    Wednesday = new DateTime(1,1,1,19,0,0),
                    Thursday = new DateTime(1,1,1,18,0,0),
                    Friday = new DateTime(1,1,1,16,0,0),
                    Saturday = null,
                    Sunday = null,
                },
                new OpeningHour
                {
                    RestaurantId = 2,
                    Monday = new DateTime(1,1,1,8,0,0),
                    Tuesday = new DateTime(1,1,1,8,0,0),
                    Wednesday = null,
                    Thursday = null,
                    Friday = new DateTime(1,1,1,8,0,0),
                    Saturday = new DateTime(1,1,1,10,0,0),
                    Sunday = new DateTime(1,1,1,10,0,0),
                },
                new OpeningHour
                {
                    RestaurantId = 2,
                    Monday = new DateTime(1,1,1,20,0,0),
                    Tuesday = new DateTime(1,1,1,20,0,0),
                    Wednesday = null,
                    Thursday = null,
                    Friday = new DateTime(1,1,1,20,0,0),
                    Saturday = new DateTime(1,1,1,16,0,0),
                    Sunday = new DateTime(1,1,1,16,0,0),
                },
            };

            foreach (OpeningHour o in openingHours)
            {
                _context.OpeningHours.Add(o);
            }

            _context.SaveChanges();
        }

    }
}
