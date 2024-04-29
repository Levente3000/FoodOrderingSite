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
            .AsNoTracking()
            .FirstOrDefault();
    }

    public void CreateProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
}