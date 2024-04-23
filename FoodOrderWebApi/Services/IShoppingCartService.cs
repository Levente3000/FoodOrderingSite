using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.ShoppingCart;

namespace FoodOrderWebApi.Services;

public interface IShoppingCartService
{
    public void AddProduct(string userId, ShoppingCartProductDto shoppingCartItemDto);
    public void UpdateQuantity(UpdateItemQuantityDto updateItemQuantity);
    public void RemoveProduct(int productId);
    public List<ShoppingCartItemDto> GetCartByUserId(string userId);
    public void ClearCart(string userId);
}