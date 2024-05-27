using System.Security.Claims;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.CreateRestaurant;
using FoodOrderWebApi.Services;
using FoodOrderWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrderWebApi.Controllers;

[Route("restaurant")]
[ApiController]
public class RestaurantController : Controller
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public List<RestaurantDto> GetAllRestaurants()
    {
        return _restaurantService.GetAllRestaurantsWithProductsAndCategories();
    }

    [HttpGet("latest")]
    public List<RestaurantDto> GetLatestRestaurants()
    {
        return _restaurantService.GetLatestRestaurants();
    }

    [HttpGet("most-orders")]
    public List<RestaurantDto> GetRestaurantsWithTheMostOrders()
    {
        return _restaurantService.GetRestaurantsWithTheMostOrders();
    }

    [HttpGet("by-category/{categoryName}")]
    public List<RestaurantDto> GetRestaurantsByCategory(string categoryName)
    {
        return _restaurantService.GetRestaurantsByCategory(categoryName);
    }

    [HttpGet("details/{id}")]
    public RestaurantDetailsDto GetRestaurantById(int id)
    {
        return _restaurantService.GetRestaurantByIdWithProductsAndCategories(id);
    }

    [HttpGet("edit-details/{id}")]
    public CreateEditRestaurantDto GetRestaurantByIdForEdit(int id)
    {
        return _restaurantService.GetRestaurantByIdForEdit(id);
    }

    [HttpPost("create-restaurant")]
    public Task<int> CreateRestaurant([FromForm] CreateEditRestaurantDto createEditRestaurant)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId.IsNullOrEmpty())
        {
            return null;
        }

        return _restaurantService.CreateRestaurant(createEditRestaurant, userId);
    }

    [HttpPost("edit-restaurant")]
    public Task<int?> EditRestaurant([FromForm] CreateEditRestaurantDto createEditRestaurant)
    {
        return _restaurantService.EditRestaurant(createEditRestaurant);
    }
}