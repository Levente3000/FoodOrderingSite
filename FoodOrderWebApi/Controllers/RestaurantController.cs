using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Services;
using Microsoft.AspNetCore.Mvc;

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
}