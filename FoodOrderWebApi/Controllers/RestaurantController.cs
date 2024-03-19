using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Controllers;

[Route("restaurant")]
[ApiController]
public class RestaurantController : Controller
{
    private readonly IRepository<Restaurant, int> _restaurantRepository;
    private readonly RestaurantService _restaurantService;
    private readonly IMapper _mapper;

    public RestaurantController(IRepository<Restaurant, int> restaurantRepository, RestaurantService restaurantService,
        IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _restaurantService = restaurantService;
        _mapper = mapper;
    }

    [HttpGet]
    public List<RestaurantDto> GetAllRestaurants()
    {
        var restaurants = _restaurantService.GetAllRestaurantsWithProductsAndCategories();

        return _mapper.Map<List<RestaurantDto>>(restaurants);
    }
}