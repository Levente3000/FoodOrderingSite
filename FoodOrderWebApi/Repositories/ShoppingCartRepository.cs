using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly FoodOrderDbContext _context;

    public ShoppingCartRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public List<ShoppingCartItem> GetAllItemByUserId(string userId)
    {
        return _context.ShoppingCartItems
            .Where(item => item.UserId == userId)
            .Include(item => item.Product)
            .ThenInclude(p => p.Restaurant)
            .AsNoTracking()
            .ToList();
    }

    public ShoppingCartItem? GetItemByCartItemId(int shoppingCartItemId)
    {
        return _context.ShoppingCartItems
            .FirstOrDefault(item => item.ShoppingCartItemId == shoppingCartItemId);
    }

    public ShoppingCartItem? GetItemByUserIdAndProductId(string userId, int productId)
    {
        return _context.ShoppingCartItems
            .FirstOrDefault(item => item.UserId == userId && item.ProductId == productId);
    }

    public void AddProduct(string userId, int productId, int quantity)
    {
        _context.ShoppingCartItems.Add(new ShoppingCartItem
        {
            UserId = userId,
            ProductId = productId,
            Quantity = quantity
        });
        _context.SaveChanges();
    }

    public void UpdateProduct(ShoppingCartItem shoppingCartItem)
    {
        _context.ShoppingCartItems.Update(shoppingCartItem);
        _context.SaveChanges();
    }

    public void RemoveProduct(ShoppingCartItem shoppingCartItem)
    {
        _context.ShoppingCartItems.Remove(shoppingCartItem);
        _context.SaveChanges();
    }

    public void ClearCart(string userId)
    {
        var products =
            _context.ShoppingCartItems.Where(item => item.UserId == userId).ToList();

        _context.ShoppingCartItems.RemoveRange(products);
        _context.SaveChanges();
    }
}