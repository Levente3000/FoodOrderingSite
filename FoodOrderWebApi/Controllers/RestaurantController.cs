using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Controllers;

[Route("restaurant")]
[ApiController]
public class RestaurantController : Controller
{
    private readonly IRepository<Restaurant, int> _restaurantRepository;

    public RestaurantController(IRepository<Restaurant, int> restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    [HttpGet]
    public List<Restaurant> GetAllRestaurants()
    {
        return _restaurantRepository.GetAll();
    }
}