using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Enum;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly FoodOrderDbContext _context;

    public RestaurantRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public List<Restaurant> GetAll()
    {
        return _context.Restaurants
            .Include(r => r.Products)
            .ThenInclude(p => p.Categories)
            .AsNoTracking()
            .ToList();
    }

    public List<Restaurant> GetLatestRestaurants()
    {
        return _context.Restaurants
            .OrderByDescending(restaurant => restaurant.CreatedAt)
            .Take(10)
            .AsNoTracking()
            .ToList();
    }

    public List<Restaurant> GetRestaurantsWithTheMostOrders(List<int> restaurantIdList)
    {
        return _context.Restaurants
            .Where(r => restaurantIdList.Contains(r.Id))
            .AsNoTracking()
            .ToList();
    }

    public List<Restaurant> GetRestaurantsByCategory(string categoryName)
    {
        return _context.Restaurants
            .Where(r => r.Products
                .Any(p => p.Categories
                    .Any(c => c.Name == categoryName)))
            .Include(r => r.Products)
            .ThenInclude(p => p.Categories)
            .AsNoTracking()
            .ToList();
    }

    public string? GetRestaurantNameById(int restaurantId)
    {
        return _context.Restaurants
            .FirstOrDefault(r => r.Id == restaurantId)
            ?.Name;
    }

    public Restaurant? GetByIdOrName(int key)
    {
        return _context.Restaurants
            .Where(r => r.Id == key)
            .Include(r => r.OpeningHours)
            .Include(r => r.ClosingHours)
            .AsNoTracking()
            .FirstOrDefault();
    }

    public Restaurant? GetByIdAsTracking(int key)
    {
        return _context.Restaurants
            .Where(r => r.Id == key)
            .Include(r => r.OpeningHours)
            .Include(r => r.ClosingHours)
            .FirstOrDefault();
    }

    public ICollection<FoodCategory> GetRestaurantFoodCategoriesWithProducts(int restaurantId)
    {
        return _context.FoodCategories
            .Where(c => c.Products.Any(p => p.RestaurantId == restaurantId))
            .Include(c => c.Products.Where(product => product.RestaurantId == restaurantId))
            .AsNoTracking()
            .ToList();
    }

    public void CreateRestaurant(Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);
        _context.SaveChanges();
    }

    public void UpdateRestaurant(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        _context.SaveChanges();
    }
}