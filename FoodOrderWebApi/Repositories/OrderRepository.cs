using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace FoodOrderWebApi.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly FoodOrderDbContext _context;

    public OrderRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public List<Order> GetOrdersByRestaurantId(int restaurantId)
    {
        var currentYear = LocalDate.FromDateTime(DateTime.UtcNow).Year;

        var orders = _context.Orders
            .Where(order => order.RestaurantId == restaurantId)
            .Include(o => o.OrderItems)
            .ThenInclude(orderItems => orderItems.Product)
            .AsNoTracking()
            .ToList();

        return orders.Where(o => o.CreatedAt.InUtc().Year == currentYear).ToList();
    }

    public List<Order> GetActiveOrdersByRestaurantId(int restaurantId)
    {
        return _context.Orders
            .Where(o => o.RestaurantId == restaurantId && o.IsDone == false)
            .Include(o => o.OrderItems)
            .ThenInclude(orderItems => orderItems.Product)
            .AsNoTracking()
            .ToList();
    }

    public List<Order> GetDoneOrdersByRestaurantId(int restaurantId)
    {
        return _context.Orders
            .Where(o => o.RestaurantId == restaurantId && o.IsDone == true)
            .Include(o => o.OrderItems)
            .ThenInclude(orderItems => orderItems.Product)
            .AsNoTracking()
            .ToList();
    }

    public List<int> GetRestaurantIdsByOrderNumber()
    {
        return _context.Orders
            .GroupBy(o => o.RestaurantId)
            .OrderByDescending(o => o.Count())
            .Select(o => o.Key)
            .Take(10)
            .ToList();
    }

    public Order? GetOrderById(int orderId)
    {
        return _context.Orders
            .FirstOrDefault(o => o.Id == orderId);
    }

    public void UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public void PlaceOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void DeleteOrder(Order order)
    {
        _context.Orders.Remove(order);
        _context.SaveChanges();
    }
}