using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IRestaurantRepository : IRepository<Restaurant, int>
{
    public List<Restaurant> GetLatestRestaurants();

    public List<Restaurant> GetRestaurantsWithTheMostOrders(List<int> restaurantIdList);

    public List<Restaurant> GetRestaurantsByCategory(string categoryName);

    public ICollection<FoodCategory>? GetRestaurantFoodCategoriesWithProducts(int restaurantId);

    public string? GetRestaurantNameById(int restaurantId);

    public void CreateRestaurant(Restaurant restaurant);

    public Restaurant? GetByIdAsTracking(int key);

    public void UpdateRestaurant(Restaurant restaurant);
}