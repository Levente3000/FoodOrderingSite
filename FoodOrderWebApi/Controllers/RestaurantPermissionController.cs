using System.Security.Claims;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrderWebApi.Controllers;

[Route("restaurant-permission")]
[ApiController]
public class RestaurantPermissionController : Controller
{
    private readonly IRestaurantPermissionService _restaurantPermissionService;

    public RestaurantPermissionController(IRestaurantPermissionService restaurantPermissionService)
    {
        _restaurantPermissionService = restaurantPermissionService;
    }

    [HttpGet]
    public List<RestaurantPermission>? GetAllRestaurantPermissionsForUser()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return userId.IsNullOrEmpty() ? null : _restaurantPermissionService.GetAllPermissionByUserId(userId);
    }

    [HttpGet("{restaurantId}")]
    public bool GetRestaurantById(int restaurantId)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return userId.IsNullOrEmpty()
            ? false
            : _restaurantPermissionService.GetPermissionByUserIdAndRestaurantId(userId, restaurantId);
    }
}