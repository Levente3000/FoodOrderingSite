using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IProductRepository
{
    public Product? GetProductById(int id);

    public void CreateProduct(Product product);
}