using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace FoodOrderWebApi.Configuration;

public class DbInitializer
{
    private static FoodOrderDbContext _context = null!;

    public static void Initialize(IServiceProvider serviceProvider)
    {
        _context = serviceProvider.GetRequiredService<FoodOrderDbContext>();

        if (_context.FoodCategories.Any()) return;

        SeedCategories();
        SeedOpeningHours();
        SeedRestaurants();
        SeedProducts();
        SeedPromo();
        SeedRestaurantPermission();
    }

    private static void SeedCategories()
    {
        var categories = new FoodCategory[]
        {
            new()
            {
                Name = "Soup",
                PictureName = "soup.jpg"
            },
            new()
            {
                Name = "Drink",
                PictureName = "drink.jpg"
            },
            new()
            {
                Name = "Pizza",
                PictureName = "pizza.jpg"
            },
            new()
            {
                Name = "Pasta",
                PictureName = "pasta.jpg"
            },
            new()
            {
                Name = "Sushi",
                PictureName = "pizza.jpg"
            },
            new()
            {
                Name = "Pastry",
                PictureName = "pizza.jpg"
            },
            new()
            {
                Name = "Asian",
                PictureName = "pizza.jpg"
            },
            new()
            {
                Name = "Exotic",
                PictureName = "pizza.jpg"
            },
            new()
            {
                Name = "Salad",
                PictureName = "pizza.jpg"
            },
            new()
            {
                Name = "Seafood",
                PictureName = "pizza.jpg"
            },
            new()
            {
                Name = "Fine Dining",
                PictureName = "pizza.jpg"
            },
        };

        _context.FoodCategories.AddRange(categories);

        _context.SaveChanges();
    }

    private static void SeedRestaurants()
    {
        var restaurants = new Restaurant[]
        {
            new()
            {
                Name = "Karen bar",
                Description = "something",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000001",
                LogoName = "karen_bar.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 1,
                ClosingHourId = 2
            },
            new()
            {
                Name = "Blue Parrot",
                Description = "a long description for a restaurant which will be displayed in a card",
                Address = "Budapest, Wesselényi street",
                PhoneNumber = "06300000002",
                LogoName = "blue_parrot.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 3,
                ClosingHourId = 4
            },
            new()
            {
                Name = "Red Dragon",
                Description = "Sushi and Japanese cuisine",
                Address = "New York, Times Square",
                PhoneNumber = "06300000003",
                LogoName = "karen_bar.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 1,
                ClosingHourId = 2
            },
            new()
            {
                Name = "Golden Spoon",
                Description = "Delicious desserts and pastries",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000004",
                LogoName = "karen_bar.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 1,
                ClosingHourId = 2
            },
            new()
            {
                Name = "Orange Orchid",
                Description = "Fresh and healthy salads",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000001",
                LogoName = "karen_bar.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 1,
                ClosingHourId = 2
            },
            new()
            {
                Name = "Black Raven",
                Description = "Pub with a selection of craft beers",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000001",
                LogoName = "karen_bar.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 1,
                ClosingHourId = 2
            },
            new()
            {
                Name = "Pink Flamingo",
                Description = "Tropical cocktails and seafood",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000001",
                LogoName = "karen_bar.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 1,
                ClosingHourId = 2
            },
            new()
            {
                Name = "Crimson Fox",
                Description = "Fine dining experience",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000001",
                LogoName = "karen_bar.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 1,
                ClosingHourId = 2
            },
        };

        _context.Restaurants.AddRange(restaurants);

        _context.SaveChanges();
    }

    private static void SeedProducts()
    {
        var categories = _context.FoodCategories.ToList();
        var products = new Product[]
        {
            new()
            {
                Name = "Goulash Soup",
                Description = "0,5L soup",
                Price = 1200,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[9] },
                RestaurantId = 1,
                IsEnabled = true,
            },
            new()
            {
                Name = "Margherita pizza",
                Description = "32cm",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[6] },
                RestaurantId = 1,
                IsEnabled = true,
            },
            new()
            {
                Name = "BBQ pizza",
                Description = "32cm",
                Price = 2300,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[6] },
                RestaurantId = 1,
                IsEnabled = true,
            },
            new()
            {
                Name = "Pesto pasta",
                Description = "one serving",
                Price = 1800,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[4] },
                RestaurantId = 2,
                IsEnabled = true,
            },
            new()
            {
                Name = "Bolognese pasta",
                Description = "one serving",
                Price = 1900,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[4] },
                RestaurantId = 2,
                IsEnabled = true,
            },
            new()
            {
                Name = "Coca Cola",
                Description = "0,33L",
                Price = 600,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[1] },
                RestaurantId = 2,
                IsEnabled = true,
            },
            new()
            {
                Name = "Sushi Platter",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[0], categories[10] },
                RestaurantId = 3
            },
            new()
            {
                Name = "Vegetarian Sushi Roll",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[0], categories[10] },
                RestaurantId = 3,
                IsEnabled = true,
            },
            new()
            {
                Name = "Greek Salad",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[7] },
                RestaurantId = 5,
                IsEnabled = true,
            },
            new()
            {
                Name = "Caesar Salad",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[7] },
                RestaurantId = 5,
                IsEnabled = true,
            },
            new()
            {
                Name = "Grilled Salmon",
                Description = "Salmon fillet grilled to perfection, served with steamed vegetables",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[8], categories[3] },
                RestaurantId = 7,
                IsEnabled = true,
            },

            new()
            {
                Name = "Lobster Linguine",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[8], categories[3] },
                RestaurantId = 7,
                IsEnabled = true,
            },
            new()
            {
                Name = "IPA",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[1] },
                RestaurantId = 6
            },
            new()
            {
                Name = "Stout",
                Description = "A dark, rich beer with flavors of roasted malt, chocolate, and coffee.",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[1] },
                RestaurantId = 6,
                IsEnabled = true,
            },
            new()
            {
                Name = "Croissant",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[5] },
                RestaurantId = 4,
                IsEnabled = true,
            },
            new()
            {
                Name = "Danish Pastry",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[5] },
                RestaurantId = 4,
                IsEnabled = true,
            },
            new()
            {
                Name = "Filet Mignon",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[3] },
                RestaurantId = 8,
                IsEnabled = true,
            },
            new()
            {
                Name = "Lobster Risotto",
                Description = "",
                Price = 2000,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory> { categories[3] },
                RestaurantId = 8,
                IsEnabled = true,
            },
        };

        _context.Products.AddRange(products);

        _context.SaveChanges();
    }

    private static void SeedOpeningHours()
    {
        var openingHours = new OpeningHour[]
        {
            new()
            {
                Monday = Instant.FromUtc(1, 1, 1, 8, 0, 0),
                Tuesday = Instant.FromUtc(1, 1, 1, 8, 0, 0),
                Wednesday = Instant.FromUtc(1, 1, 1, 8, 0, 0),
                Thursday = Instant.FromUtc(1, 1, 1, 8, 0, 0),
                Friday = Instant.FromUtc(1, 1, 1, 8, 0, 0),
                Saturday = null,
                Sunday = null
            },
            new()
            {
                Monday = Instant.FromUtc(1, 1, 1, 18, 0, 0),
                Tuesday = Instant.FromUtc(1, 1, 1, 20, 0, 0),
                Wednesday = Instant.FromUtc(1, 1, 1, 19, 0, 0),
                Thursday = Instant.FromUtc(1, 1, 1, 18, 0, 0),
                Friday = Instant.FromUtc(1, 1, 1, 16, 0, 0),
                Saturday = null,
                Sunday = null
            },
            new()
            {
                Monday = Instant.FromUtc(1, 1, 1, 8, 0, 0),
                Tuesday = Instant.FromUtc(1, 1, 1, 8, 0, 0),
                Wednesday = null,
                Thursday = null,
                Friday = Instant.FromUtc(1, 1, 1, 8, 0, 0),
                Saturday = Instant.FromUtc(1, 1, 1, 10, 0, 0),
                Sunday = Instant.FromUtc(1, 1, 1, 10, 0, 0)
            },
            new()
            {
                Monday = Instant.FromUtc(1, 1, 1, 20, 0, 0),
                Tuesday = Instant.FromUtc(1, 1, 1, 20, 0, 0),
                Wednesday = null,
                Thursday = null,
                Friday = Instant.FromUtc(1, 1, 1, 20, 0, 0),
                Saturday = Instant.FromUtc(1, 1, 1, 16, 0, 0),
                Sunday = Instant.FromUtc(1, 1, 1, 16, 0, 0)
            }
        };

        _context.OpeningHours.AddRange(openingHours);

        _context.SaveChanges();
    }

    private static void SeedPromo()
    {
        var promoCodes = new PromoCode[]
        {
            new()
            {
                Code = "promo",
                Percentage = 0.2
            },
        };

        _context.PromoCodes.AddRange(promoCodes);

        _context.SaveChanges();
    }

    private static void SeedRestaurantPermission()
    {
        var restaurantPermissions = new RestaurantPermission[]
        {
            new()
            {
                RestaurantId = 1,
                UserId = "e79a7435-8c69-469c-b831-66aad159aa33"
            },
            new()
            {
                RestaurantId = 2,
                UserId = "e79a7435-8c69-469c-b831-66aad159aa33"
            },
        };

        _context.RestaurantPermissions.AddRange(restaurantPermissions);

        _context.SaveChanges();
    }
}