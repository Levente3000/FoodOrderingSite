namespace FoodOrderWebApi.DTOs;

public class ShoppingCartItemDto
{
    public int ShoppingCartItemId { get; set; }

    public string UserId { get; set; }

    public int Quantity { get; set; }

    public string? RestaurantName { get; set; } = null!;

    public int ProductId { get; set; }

    public virtual ProductDto Product { get; set; } = null!;
}