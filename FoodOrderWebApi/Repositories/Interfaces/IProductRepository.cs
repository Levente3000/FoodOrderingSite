using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IProductRepository
{
    public Product? GetProductById(int id);

    public Product? GetProductByIdAsTracking(int id);

    public double GetAveragePriceByRestaurantId(int restaurantId);

    public bool GetIfAnyProductUnderRestaurant(int restaurantId);

    public List<Product> GetProductWithoutCategoryByRestaurantId(int restaurantId);

    public void CreateProduct(Product product);

    public void UpdateProduct(Product product);
}