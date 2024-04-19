using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IRestaurantRepository : IRepository<Restaurant, int>
{
    public ICollection<FoodCategory>? GetRestaurantFoodCategoriesWithProducts(int restaurantId);

    public void CreateRestaurant(Restaurant restaurant);

    public Restaurant? GetByIdAsTracking(int key);

    public void UpdateRestaurant(Restaurant restaurant);
}