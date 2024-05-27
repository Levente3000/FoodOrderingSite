using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services.Interfaces;

namespace FoodOrderWebApi.Services;

public class RestaurantPermissionService : IRestaurantPermissionService
{
    private readonly IRestaurantPermissionRepository _restaurantPermissionRepository;
    private readonly IKeycloakService _keycloakService;

    public RestaurantPermissionService(IRestaurantPermissionRepository restaurantPermissionRepository,
        IKeycloakService keycloakService)
    {
        _restaurantPermissionRepository = restaurantPermissionRepository;
        _keycloakService = keycloakService;
    }

    public List<RestaurantPermission> GetAllPermissionByUserId(string userId)
    {
        return _restaurantPermissionRepository.GetAllPermissionByUserId(userId);
    }

    public bool GetPermissionByUserIdAndRestaurantId(string userId, int restaurantId)
    {
        return _restaurantPermissionRepository.GetPermissionByUserIdAndRestaurantId(userId, restaurantId);
    }

    public async void AddPermissionToUser(string userId, RestaurantPermission restaurantPermission)
    {
        _restaurantPermissionRepository.AddPermissionToUser(restaurantPermission);
        await _keycloakService.AssignRestaurantOwnerRoleToUserAsync(userId);
    }
}