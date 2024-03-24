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
    private readonly RestaurantService _restaurantService;
    private readonly IMapper _mapper;

    public RestaurantController(RestaurantService restaurantService,
        IMapper mapper)
    {
        _restaurantService = restaurantService;
        _mapper = mapper;
    }

    [HttpGet]
    public List<RestaurantDto> GetAllRestaurants()
    {
        var restaurants = _restaurantService.GetAllRestaurantsWithProductsAndCategories();

        return restaurants;
    }

    [HttpGet("details/{id}")]
    public RestaurantDto GetRestaurantById(int id)
    {
        var restaurant = _restaurantService.GetRestaurantByIdWithProductsAndCategories(id);

        return restaurant;
    }
}