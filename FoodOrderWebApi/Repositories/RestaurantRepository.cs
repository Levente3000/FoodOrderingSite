using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class RestaurantRepository : IRepository<Restaurant, int>
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
            .AsNoTracking()
            .FirstOrDefault();
    }

    public List<Restaurant> GetAllRestaurantsWithProductsAndCategories()
    {
        return _context.Restaurants
            .Include(r => r.Products)
            .ThenInclude(p => p.Categories)
            .ToList();
    }
}