using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IRestaurantPermissionRepository
{
    public List<RestaurantPermission> GetAllPermissionByUserId(string userId);

    public bool GetPermissionByUserIdAndRestaurantId(string userId, int restaurantId);

    public void AddPermissionToUser(RestaurantPermission restaurantPermission);
}