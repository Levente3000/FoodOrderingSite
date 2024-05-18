using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IRestaurantPermissionService
{
    public List<RestaurantPermission> GetAllPermissionByUserId(string userId);

    public bool GetPermissionByUserIdAndRestaurantId(string userId, int restaurantId);

    public void AddPermissionToUser(string userId, RestaurantPermission restaurantPermission);
}