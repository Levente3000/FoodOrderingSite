using System.Security.Claims;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.CreateRestaurant;
using FoodOrderWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrderWebApi.Controllers;

[Route("restaurant")]
[ApiController]
public class RestaurantController : Controller
{
    private readonly RestaurantService _restaurantService;

    public RestaurantController(RestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public List<RestaurantDto> GetAllRestaurants()
    {
        return _restaurantService.GetAllRestaurantsWithProductsAndCategories();
    }

    [HttpGet("details/{id}")]
    public RestaurantDetailsDto GetRestaurantById(int id)
    {
        var a = _restaurantService.GetRestaurantByIdWithProductsAndCategories(id);
        return a;
    }

    [HttpGet("edit-details/{id}")]
    public CreateEditRestaurantDto GetRestaurantByIdForEdit(int id)
    {
        return _restaurantService.GetRestaurantByIdForEdit(id);
    }

    [HttpPost("create-restaurant")]
    public int? CreateRestaurant([FromForm] CreateEditRestaurantDto createEditRestaurant)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId.IsNullOrEmpty())
        {
            return null;
        }

        return _restaurantService.CreateRestaurant(createEditRestaurant, userId);
    }

    [HttpPost("edit-restaurant")]
    public int? EditRestaurant([FromForm] CreateEditRestaurantDto createEditRestaurant)
    {
        return _restaurantService.EditRestaurant(createEditRestaurant);
    }
}