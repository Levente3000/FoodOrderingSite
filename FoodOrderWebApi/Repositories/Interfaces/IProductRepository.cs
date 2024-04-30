using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IProductRepository
{
    public Product? GetProductById(int id);

    public Product? GetProductByIdAsTracking(int id);

    public List<Product> GetProductWithoutCategoryByRestaurantId(int restaurantId);

    public void CreateProduct(Product product);

    public void UpdateProduct(Product product);
}