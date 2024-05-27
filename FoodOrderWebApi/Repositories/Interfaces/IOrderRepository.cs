using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IOrderRepository
{
    public List<Order> GetOrdersByRestaurantId(int restaurantId);

    public List<Order> GetActiveOrdersByRestaurantId(int restaurantId);

    public List<Order> GetDoneOrdersByRestaurantId(int restaurantId);

    public List<int> GetRestaurantIdsByOrderNumber();

    public Order? GetOrderById(int orderId);

    public void UpdateOrder(Order order);

    public void PlaceOrder(Order order);

    public void DeleteOrder(Order order);
}