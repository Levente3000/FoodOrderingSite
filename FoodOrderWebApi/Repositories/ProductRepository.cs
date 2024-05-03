using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly FoodOrderDbContext _context;

    public ProductRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public Product? GetProductById(int id)
    {
        return _context.Products
            .Where(product => product.Id == id)
            .Include(product => product.Categories)
            .AsNoTracking()
            .FirstOrDefault();
    }

    public Product? GetProductByIdAsTracking(int id)
    {
        return _context.Products
            .Where(product => product.Id == id)
            .Include(product => product.Categories)
            .FirstOrDefault();
    }

    public double GetAveragePriceByRestaurantId(int restaurantId)
    {
        return _context.Products
            .Where(product => product.RestaurantId == restaurantId)
            .Average(product => product.Price);
    }

    public bool GetIfAnyProductUnderRestaurant(int restaurantId)
    {
        return _context.Products
            .Any(p => p.RestaurantId == restaurantId);
    }

    public List<Product> GetProductWithoutCategoryByRestaurantId(int restaurantId)
    {
        return _context.Products
            .Where(product => product.RestaurantId == restaurantId && product.Categories.Count == 0)
            .ToList();
    }

    public void CreateProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }
}