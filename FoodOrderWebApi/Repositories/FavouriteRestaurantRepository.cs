using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class FavouriteRestaurantRepository : IFavouriteRestaurantRepository
{
    private readonly FoodOrderDbContext _context;

    public FavouriteRestaurantRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public FavouriteRestaurant? GetFavouriteRestaurantByUserIdAndRestaurantId(string userId, int restaurantId)
    {
        return _context.FavouriteRestaurants
            .FirstOrDefault(r => r.userId == userId && r.RestaurantId == restaurantId);
    }

    public List<FavouriteRestaurant> GetAllFavouriteRestaurant(string userId)
    {
        return _context.FavouriteRestaurants
            .Where(r => r.userId == userId)
            .Include(r => r.Restaurant)
            .AsNoTracking()
            .ToList();
    }

    public void AddFavouriteRestaurant(FavouriteRestaurant favouriteRestaurant)
    {
        _context.FavouriteRestaurants.Add(favouriteRestaurant);
        _context.SaveChanges();
    }

    public void RemoveFavouriteRestaurant(FavouriteRestaurant favouriteRestaurant)
    {
        _context.FavouriteRestaurants.Remove(favouriteRestaurant);
        _context.SaveChanges();
    }
}