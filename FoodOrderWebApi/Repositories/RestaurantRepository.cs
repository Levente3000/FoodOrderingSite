using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
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

    public Restaurant? GetByIdOrName(int key)
    {
        return _context.Restaurants
            .Where(r => r.Id == key)
            .Include(r => r.OpeningHours)
            .Include(r => r.ClosingHours)
            .AsNoTracking()
            .FirstOrDefault();
    }

    public ICollection<FoodCategory> GetRestaurantFoodCategoriesWithProducts(int restaurantId)
    {
        return _context.FoodCategories
            .Where(c => c.Products.Any(p => p.RestaurantId == restaurantId))
            .Include(c => c.Products)
            .AsNoTracking()
            .ToList();
    }
}