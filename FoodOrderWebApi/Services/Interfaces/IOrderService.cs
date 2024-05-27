using FoodOrderWebApi.DTOs.Order;
using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IOrderService
{
    public List<OrderDto> GetOrdersByRestaurantId(int restaurantId);

    public List<OrderDto> GetActiveOrdersByRestaurantId(int restaurantId);

    public List<OrderDto> GetDoneOrdersByRestaurantId(int restaurantId);

    public List<int> GetRestaurantIdsByOrderNumber();

    public void PlaceOrder(string userId, PromoCode? promo);

    public void UpdateOrder(int orderId);

    public void DeleteOrder();
}