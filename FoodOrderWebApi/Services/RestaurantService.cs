using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;

namespace FoodOrderWebApi.Services;

public class RestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public List<RestaurantDto> GetAllRestaurantsWithProductsAndCategories()
    {
        var restaurants = _restaurantRepository.GetAll();
        return _mapper.Map<List<RestaurantDto>>(restaurants);
    }

    public RestaurantDetailsDto GetRestaurantByIdWithProductsAndCategories(int id)
    {
        var restaurant = _mapper.Map<RestaurantDetailsDto>(_restaurantRepository.GetByIdOrName(id));
        restaurant.CategoriesWithProducts =
            _mapper.Map<ICollection<ProductsInCategoryDto>>(_restaurantRepository
                .GetRestaurantFoodCategoriesWithProducts(id));
        return restaurant;
    }
}