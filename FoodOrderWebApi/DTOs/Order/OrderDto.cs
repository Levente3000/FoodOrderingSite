using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }

    public string OrdererName { get; set; } = null!;

    public string OrdererAddress { get; set; } = null!;

    public string OrdererPhoneNumber { get; set; } = null!;

    public bool IsDone { get; set; }

    public int RestaurantId { get; set; }

    public List<OrderItemDto> OrderItems { get; set; } = null!;
}