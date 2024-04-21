using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Services;

public interface IRestaurantPermissionService
{
    public List<RestaurantPermission> GetAllPermissionByUserId(string userId);

    public bool GetPermissionByUserIdAndRestaurantId(string userId, int restaurantId);
}