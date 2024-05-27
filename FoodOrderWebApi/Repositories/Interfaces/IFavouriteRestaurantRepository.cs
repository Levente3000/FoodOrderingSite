using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IFavouriteRestaurantRepository
{
    public FavouriteRestaurant? GetFavouriteRestaurantByUserIdAndRestaurantId(string userId, int restaurantId);

    public List<FavouriteRestaurant> GetAllFavouriteRestaurant(string userId);

    public void AddFavouriteRestaurant(FavouriteRestaurant favouriteRestaurant);

    public void RemoveFavouriteRestaurant(FavouriteRestaurant favouriteRestaurant);
}