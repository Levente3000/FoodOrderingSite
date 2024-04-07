using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Services;

public interface IShoppingCartService
{
    public void AddProduct(string userId, int productId, int quantity);
    public void RemoveOneProduct(string userId, int productId);
    public void RemoveProduct(string userId, int productId);
    public List<ShoppingCartItemDto> GetCartByUserId(string userId);
    public void ClearCart(string userId);
}