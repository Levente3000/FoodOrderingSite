using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories;

public interface IRestaurantRepository : IRepository<Restaurant, int>
{
    public ICollection<FoodCategory>? GetRestaurantFoodCategoriesWithProducts(int restaurantId);
}