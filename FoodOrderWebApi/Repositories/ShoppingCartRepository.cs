using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class ShoppingCartRepository
{
    private readonly FoodOrderDbContext _context;

    public ShoppingCartRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public List<ShoppingCartItem> GetAllItemByUserId(string userId)
    {
        return _context.ShoppingCartItem
            .Where(item => item.UserId == userId)
            .AsNoTracking()
            .ToList();
    }

    public void AddProduct(string userId, int productId, int quantity)
    {
        _context.ShoppingCartItem
            .Add(new ShoppingCartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
            });
        _context.SaveChanges();
    }

    public void RemoveOneProduct(string userId, int productId)
    {
        var product =
            _context.ShoppingCartItem.FirstOrDefault(item => item.UserId == userId && item.ProductId == productId);

        if (product == null)
        {
            return;
        }

        product.Quantity--;

        _context.ShoppingCartItem.Update(product);
        _context.SaveChanges();
    }

    public void RemoveProduct(string userId, int productId)
    {
        var product =
            _context.ShoppingCartItem.FirstOrDefault(item => item.UserId == userId && item.ProductId == productId);

        if (product == null)
        {
            return;
        }

        _context.ShoppingCartItem.Remove(product);
        _context.SaveChanges();
    }

    public void ClearCart(string userId)
    {
        var products =
            _context.ShoppingCartItem.Where(item => item.UserId == userId).ToList();

        _context.ShoppingCartItem.RemoveRange(products);
        _context.SaveChanges();
    }
}