using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Repositories.Interfaces;

namespace FoodOrderWebApi.Services;

public class RestaurantPermissionService : IRestaurantPermissionService
{
    private readonly IRestaurantPermissionRepository _restaurantPermissionRepository;

    public RestaurantPermissionService(IRestaurantPermissionRepository restaurantPermissionRepository)
    {
        _restaurantPermissionRepository = restaurantPermissionRepository;
    }

    public List<RestaurantPermission> GetAllPermissionByUserId(string userId)
    {
        return _restaurantPermissionRepository.GetAllPermissionByUserId(userId);
    }

    public bool GetPermissionByUserIdAndRestaurantId(string userId, int restaurantId)
    {
        return _restaurantPermissionRepository.GetPermissionByUserIdAndRestaurantId(userId, restaurantId);
    }
}