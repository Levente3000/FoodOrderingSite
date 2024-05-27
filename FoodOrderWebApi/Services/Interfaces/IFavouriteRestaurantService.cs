using FoodOrderWebApi.DTOs;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IFavouriteRestaurantService
{
    public List<RestaurantDto> GetAllFavouriteRestaurant(string userId);

    public bool GetIsRestaurantInFavourites(string userId, int restaurantId);

    public void ChangeStateOfFavouriteRestaurant(string userId, int restaurantId);
}