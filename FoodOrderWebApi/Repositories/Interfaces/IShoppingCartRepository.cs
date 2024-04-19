using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories;

public interface IShoppingCartRepository
{
    public List<ShoppingCartItem> GetAllItemByUserId(string userId);

    public void AddProduct(string userId, int productId, int quantity);

    public void RemoveOneProduct(string userId, int productId);

    public void UpdateQuantity(int shoppingCartItemId, int quantity);

    public void RemoveProduct(int itemid);

    public void ClearCart(string userId);
}