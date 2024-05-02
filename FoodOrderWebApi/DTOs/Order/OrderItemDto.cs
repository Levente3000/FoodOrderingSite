namespace FoodOrderWebApi.DTOs.Order;

public class OrderItemDto
{
    public ProductDto Product { get; set; } = null!;

    public int Quantity { get; set; }
}