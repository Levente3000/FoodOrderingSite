using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class RestaurantPermissionRepository : IRestaurantPermissionRepository
{
    private readonly FoodOrderDbContext _context;

    public RestaurantPermissionRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public List<RestaurantPermission> GetAllPermissionByUserId(string userId)
    {
        return _context.RestaurantPermissions
            .Where(permission => permission.UserId == userId)
            .AsNoTracking()
            .ToList();
    }

    public bool GetPermissionByUserIdAndRestaurantId(string userId, int restaurantId)
    {
        return _context.RestaurantPermissions
            .Any(permission => permission.UserId == userId && permission.RestaurantId == restaurantId);
    }

    public void AddPermissionToUser(RestaurantPermission restaurantPermission)
    {
        _context.RestaurantPermissions.Add(restaurantPermission);
        _context.SaveChanges();
    }
}