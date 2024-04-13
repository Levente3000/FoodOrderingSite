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
            .AsNoTracking()
            .ToList();
    }

    public void AddProduct(string userId, int productId, int quantity)
    {
        var item = _context.ShoppingCartItems.FirstOrDefault(
            item => item.UserId == userId && item.ProductId == productId);

        if (item == null)
        {
            _context.ShoppingCartItems
                .Add(new ShoppingCartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                });
        }
        else
        {
            item.Quantity += quantity;
            _context.ShoppingCartItems.Update(item);
        }

        _context.SaveChanges();
    }

    public void RemoveOneProduct(string userId, int productId)
    {
        var product =
            _context.ShoppingCartItems.FirstOrDefault(item => item.UserId == userId && item.ProductId == productId);

        if (product == null)
        {
            return;
        }

        product.Quantity--;

        _context.ShoppingCartItems.Update(product);
        _context.SaveChanges();
    }

    public void UpdateQuantity(int shoppingCartItemId, int quantity)
    {
        var product =
            _context.ShoppingCartItems.FirstOrDefault(item => item.ShoppingCartItemId == shoppingCartItemId);

        if (product == null)
        {
            return;
        }

        product.Quantity = quantity;

        _context.ShoppingCartItems.Update(product);
        _context.SaveChanges();
    }

    public void RemoveProduct(int itemid)
    {
        var product =
            _context.ShoppingCartItems.FirstOrDefault(item => item.ShoppingCartItemId == itemid);

        if (product == null)
        {
            return;
        }

        _context.ShoppingCartItems.Remove(product);
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