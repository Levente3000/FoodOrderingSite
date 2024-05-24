using AutoMapper;
using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Services;

namespace UnitTest;

public class FavouriteRestaurantServiceTest
{
    private readonly FoodOrderDbContext _context;
    private readonly FavouriteRestaurantService _service;

    public FavouriteRestaurantServiceTest()
    {
        var options = DbContextOptions.GetOptions("TestDatabase_FavouriteRestaurantService");
        _context = new FoodOrderDbContext(options);

        // Initialize Mapper (Assuming a configuration exists)
        var config = new MapperConfiguration(cfg => { cfg.CreateMap<Restaurant, RestaurantDto>(); });
        var mapper = config.CreateMapper();

        // Repositories using the same DbContext instance
        var favouriteRestaurantRepo = new FavouriteRestaurantRepository(_context);
        var productRepo = new ProductRepository(_context);

        // Service initialization with repositories and mapper
        _service = new FavouriteRestaurantService(productRepo, favouriteRestaurantRepo, mapper);

        // Seed the database if necessary
        DbContextInitializer.InitializeForTestDb(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public void GetAllFavouriteRestaurantTest()
    {
        const string userId = "test-user";
        const int restaurantId = 1;
        _context.FavouriteRestaurants.Add(new FavouriteRestaurant { UserId = userId, RestaurantId = restaurantId });
        _context.SaveChanges();

        var result = _service.GetAllFavouriteRestaurant(userId);

        Assert.Single(result);
        Assert.Equal(restaurantId, result.First().Id);
    }

    [Fact]
    public void GetIsRestaurantInFavouritesTest()
    {
        const string userId = "test-user";
        const int restaurantId = 1;
        _context.FavouriteRestaurants.Add(new FavouriteRestaurant { UserId = userId, RestaurantId = restaurantId });
        _context.SaveChanges();

        var isFavourite = _service.GetIsRestaurantInFavourites(userId, restaurantId);

        Assert.True(isFavourite);
    }

    [Fact]
    public void ChangeStateOfFavouriteRestaurantTest_Add()
    {
        const string userId = "test-user";
        const int restaurantId = 1;

        _service.ChangeStateOfFavouriteRestaurant(userId, restaurantId);
        var favourite = _context.FavouriteRestaurants
            .FirstOrDefault(f => f.UserId == userId && f.RestaurantId == restaurantId);

        // Assert
        Assert.NotNull(favourite);
    }

    [Fact]
    public void ChangeStateOfFavouriteRestaurantTest_Remove()
    {
        const string userId = "test-user";
        const int restaurantId = 1;
        _context.FavouriteRestaurants.Add(new FavouriteRestaurant { UserId = userId, RestaurantId = restaurantId });
        _context.SaveChanges();

        _service.ChangeStateOfFavouriteRestaurant(userId, restaurantId);
        var favourite = _context.FavouriteRestaurants
            .FirstOrDefault(f => f.UserId == userId && f.RestaurantId == restaurantId);

        // Assert
        Assert.Null(favourite);
    }
}