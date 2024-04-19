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
        return _restaurantService.GetRestaurantByIdWithProductsAndCategories(id);
    }

    [HttpGet("edit-details/{id}")]
    public CreateEditRestaurantDto GetRestaurantByIdForEdit(int id)
    {
        return _restaurantService.GetRestaurantByIdForEdit(id);
    }

    [HttpPost("create-restaurant")]
    public IActionResult CreateRestaurant([FromForm] CreateEditRestaurantDto createEditRestaurant)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId.IsNullOrEmpty())
        {
            return StatusCode(401);
        }

        _restaurantService.CreateRestaurant(createEditRestaurant, userId);
        return StatusCode(201);
    }

    [HttpPost("edit-restaurant")]
    public IActionResult EditRestaurant([FromForm] CreateEditRestaurantDto createEditRestaurant)
    {
        _restaurantService.EditRestaurant(createEditRestaurant);
        return StatusCode(201);
    }
}