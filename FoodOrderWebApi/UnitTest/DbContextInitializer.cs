using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using NodaTime;

namespace UnitTest;

public class DbContextInitializer
{
    public static void InitializeForTestDb(FoodOrderDbContext foodOrderDbContext)
    {
        SeedCategories(foodOrderDbContext);
        SeedOpeningHours(foodOrderDbContext);
        SeedRestaurants(foodOrderDbContext);
        SeedProducts(foodOrderDbContext);
        SeedPromo(foodOrderDbContext);
        SeedRestaurantPermission(foodOrderDbContext);
    }

    private static void SeedCategories(FoodOrderDbContext context)
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
                PictureName = "sushi.jpg"
            },
            new()
            {
                Name = "Pastry",
                PictureName = "pastry.jpg"
            },
            new()
            {
                Name = "Asian",
                PictureName = "asian.jpg"
            },
            new()
            {
                Name = "Exotic",
                PictureName = "exotic.jpg"
            },
            new()
            {
                Name = "Salad",
                PictureName = "salad.jpg"
            },
            new()
            {
                Name = "Seafood",
                PictureName = "seafood.jpg"
            },
            new()
            {
                Name = "Fine Dining",
                PictureName = "fine_dining.jpg"
            },
            new()
            {
                Name = "Mexican",
                PictureName = "mexican.jpg"
            },
            new()
            {
                Name = "Hamburger",
                PictureName = "hamburger.jpg"
            },
            new()
            {
                Name = "American",
                PictureName = "american.jpg"
            },
        };

        context.FoodCategories.AddRange(categories);

        context.SaveChanges();
    }

    private static void SeedRestaurants(FoodOrderDbContext context)
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
                LogoName = "red_dragon.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 5,
                ClosingHourId = 6
            },
            new()
            {
                Name = "Golden Spoon",
                Description = "Delicious desserts and pastries",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000004",
                LogoName = "golden_spoon.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 7,
                ClosingHourId = 8
            },
            new()
            {
                Name = "Orange Orchid",
                Description = "Fresh and healthy salads",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000001",
                LogoName = "orange_orchid.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 9,
                ClosingHourId = 10
            },
            new()
            {
                Name = "Black Raven",
                Description = "Pub with a selection of craft beers",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000001",
                LogoName = "black_raven.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 11,
                ClosingHourId = 12
            },
            new()
            {
                Name = "Pink Flamingo",
                Description = "Tropical cocktails and seafood",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000001",
                LogoName = "pink_flamingo.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 13,
                ClosingHourId = 14
            },
            new()
            {
                Name = "Crimson Fox",
                Description = "Fine dining experience",
                Address = "Budapest, Lágymányosi campus",
                PhoneNumber = "06300000001",
                LogoName = "crimson_fox.jpg",
                BannerName = "banner.jpg",
                OpeningHourId = 15,
                ClosingHourId = 16
            },
        };

        context.Restaurants.AddRange(restaurants);

        context.SaveChanges();
    }

    private static void SeedProducts(FoodOrderDbContext context)
    {
        var categories = context.FoodCategories.ToList();
        var products = new Product[]
        {
            new()
            {
                Name = "Goulash Soup",
                Description = "0,5L soup",
                Price = 1200,
                PictureName = "goulash_soup.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Soup")),
                RestaurantId = 1,
                IsEnabled = true,
            },
            new()
            {
                Name = "Margherita pizza",
                Description = "32cm",
                Price = 2000,
                PictureName = "margherita.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Pizza")),
                RestaurantId = 1,
                IsEnabled = true,
            },
            new()
            {
                Name = "BBQ pizza",
                Description = "32cm",
                Price = 2300,
                PictureName = "bbq.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Pizza")),
                RestaurantId = 1,
                IsEnabled = true,
            },
            new()
            {
                Name = "Pesto pasta",
                Description = "one serving",
                Price = 1800,
                PictureName = "pesto_pasta.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Pasta")),
                RestaurantId = 2,
                IsEnabled = true,
            },
            new()
            {
                Name = "Bolognese pasta",
                Description = "one serving",
                Price = 1900,
                PictureName = "bolognese_pasta.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Pasta")),
                RestaurantId = 2,
                IsEnabled = true,
            },
            new()
            {
                Name = "Coca Cola",
                Description = "0,33L",
                Price = 600,
                PictureName = "coca_cola.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Drink")),
                RestaurantId = 2,
                IsEnabled = true,
            },
            new()
            {
                Name = "Sushi Platter",
                Description = "",
                Price = 2000,
                PictureName = "sushi_platter.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name is "Sushi" or "Asian")),
                RestaurantId = 3
            },
            new()
            {
                Name = "Vegetarian Sushi Roll",
                Description = "",
                Price = 2000,
                PictureName = "vegetarian_sushi.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name is "Sushi" or "Asian")),
                RestaurantId = 3,
                IsEnabled = true,
            },
            new()
            {
                Name = "Greek Salad",
                Description = "",
                Price = 2000,
                PictureName = "greek_salad.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Salad")),
                RestaurantId = 5,
                IsEnabled = true,
            },
            new()
            {
                Name = "Caesar Salad",
                Description = "",
                Price = 2000,
                PictureName = "caesar_salad.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Salad")),
                RestaurantId = 5,
                IsEnabled = true,
            },
            new()
            {
                Name = "Grilled Salmon",
                Description = "Salmon fillet grilled to perfection, served with steamed vegetables",
                Price = 2000,
                PictureName = "grilled_salmon.jpg",
                Categories =
                    new List<FoodCategory>(categories.Where(category => category.Name is "Seafood" or "Fine Dining")),
                RestaurantId = 7,
                IsEnabled = true,
            },

            new()
            {
                Name = "Lobster Linguine",
                Description = "",
                Price = 2000,
                PictureName = "lobster_linguine.png",
                Categories =
                    new List<FoodCategory>(categories.Where(category => category.Name is "Seafood" or "Fine Dining")),
                RestaurantId = 7,
                IsEnabled = true,
            },
            new()
            {
                Name = "IPA",
                Description = "",
                Price = 2000,
                PictureName = "ipa.png",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Drink")),
                RestaurantId = 6
            },
            new()
            {
                Name = "Stout",
                Description = "A dark, rich beer with flavors of roasted malt, chocolate, and coffee.",
                Price = 2000,
                PictureName = "stout.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Drink")),
                RestaurantId = 6,
                IsEnabled = true,
            },
            new()
            {
                Name = "Croissant",
                Description = "",
                Price = 2000,
                PictureName = "croissant.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Pastry")),
                RestaurantId = 4,
                IsEnabled = true,
            },
            new()
            {
                Name = "Danish Pastry",
                Description = "",
                Price = 2000,
                PictureName = "danish_pastry.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Pastry")),
                RestaurantId = 4,
                IsEnabled = true,
            },
            new()
            {
                Name = "Filet Mignon",
                Description = "",
                Price = 2000,
                PictureName = "filet_mignon.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Fine Dining")),
                RestaurantId = 8,
                IsEnabled = true,
            },
            new()
            {
                Name = "Lobster Risotto",
                Description = "",
                Price = 2000,
                PictureName = "lobster_risotto.jpg",
                Categories = new List<FoodCategory>(categories.Where(category => category.Name == "Fine Dining")),
                RestaurantId = 8,
                IsEnabled = true,
            },
        };

        context.Products.AddRange(products);

        context.SaveChanges();
    }

    private static void SeedOpeningHours(FoodOrderDbContext context)
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

        context.OpeningHours.AddRange(openingHours);

        context.SaveChanges();
    }

    private static void SeedPromo(FoodOrderDbContext context)
    {
        var promoCodes = new PromoCode[]
        {
            new()
            {
                Code = "promo",
                Percentage = 0.2
            },
        };

        context.PromoCodes.AddRange(promoCodes);

        context.SaveChanges();
    }

    private static void SeedRestaurantPermission(FoodOrderDbContext context)
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

        context.RestaurantPermissions.AddRange(restaurantPermissions);

        context.SaveChanges();
    }
}