using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories;

public interface IRestaurantPermissionRepository
{
    public List<RestaurantPermission> GetAllPermissionByUserId(string userId);

    public RestaurantPermission? GetPermissionByUserIdAndRestaurantId(string userId, int restaurantId);

    public void AddPermissionToUser(RestaurantPermission restaurantPermission);
}