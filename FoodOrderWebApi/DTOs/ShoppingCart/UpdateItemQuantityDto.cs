namespace FoodOrderWebApi.DTOs;

public class UpdateItemQuantityDto
{
    public int ShoppingCartItemId { get; set; }

    public int Quantity { get; set; }
}