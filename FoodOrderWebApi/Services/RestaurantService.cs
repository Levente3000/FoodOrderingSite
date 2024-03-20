using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;

namespace FoodOrderWebApi.Services;

public class RestaurantService
{
    private readonly IRepository<Restaurant, int> _restaurantRepository;
    private readonly IMapper _mapper;

    public RestaurantService(IRepository<Restaurant, int> restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public List<RestaurantDto> GetAllRestaurantsWithProductsAndCategories()
    {
        var restaurants = _restaurantRepository.GetAll();
        return _mapper.Map<List<RestaurantDto>>(restaurants);
    }
}