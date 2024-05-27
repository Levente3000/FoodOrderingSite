namespace FoodOrderWebApi.DTOs.ShoppingCart;

public class UpdateItemQuantityDto
{
    public int ShoppingCartItemId { get; set; }

    public int Quantity { get; set; }
}