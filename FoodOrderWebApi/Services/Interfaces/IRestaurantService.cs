using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.CreateRestaurant;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IRestaurantService
{
    public List<RestaurantDto> GetAllRestaurantsWithProductsAndCategories();

    public List<RestaurantDto> GetLatestRestaurants();

    public List<RestaurantDto> GetRestaurantsWithTheMostOrders();

    public List<RestaurantDto> GetRestaurantsByCategory(string categoryName);

    public string? GetRestaurantNameById(int restaurantId);

    public RestaurantDetailsDto GetRestaurantByIdWithProductsAndCategories(int id);

    public CreateEditRestaurantDto GetRestaurantByIdForEdit(int id);

    public Task<int> CreateRestaurant(CreateEditRestaurantDto createEditRestaurant, string userId);

    public Task<int?> EditRestaurant(CreateEditRestaurantDto createEditRestaurant);
}