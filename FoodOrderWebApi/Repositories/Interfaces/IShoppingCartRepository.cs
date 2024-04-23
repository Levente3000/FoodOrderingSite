using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories;

public interface IShoppingCartRepository
{
    public List<ShoppingCartItem> GetAllItemByUserId(string userId);

    public ShoppingCartItem? GetItemByCartItemId(int shoppingCartItemId);

    public ShoppingCartItem? GetItemByUserIdAndProductId(string userId, int productId);

    public void AddProduct(string userId, int productId, int quantity);

    public void UpdateProduct(ShoppingCartItem shoppingCartItem);

    public void RemoveProduct(ShoppingCartItem shoppingCartItem);

    public void ClearCart(string userId);
}