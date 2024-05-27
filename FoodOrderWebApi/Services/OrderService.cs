using AutoMapper;
using FoodOrderWebApi.DTOs.Order;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services.Interfaces;

namespace FoodOrderWebApi.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IUserDataService _userDataService;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IShoppingCartService shoppingCartService,
        IUserDataService userDataService, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _shoppingCartService = shoppingCartService;
        _userDataService = userDataService;
        _mapper = mapper;
    }

    public List<OrderDto> GetOrdersByRestaurantId(int restaurantId)
    {
        return _mapper.Map<List<OrderDto>>(_orderRepository.GetOrdersByRestaurantId(restaurantId));
    }

    public List<OrderDto> GetActiveOrdersByRestaurantId(int restaurantId)
    {
        return _mapper.Map<List<OrderDto>>(_orderRepository.GetActiveOrdersByRestaurantId(restaurantId));
    }

    public List<OrderDto> GetDoneOrdersByRestaurantId(int restaurantId)
    {
        return _mapper.Map<List<OrderDto>>(_orderRepository.GetDoneOrdersByRestaurantId(restaurantId));
    }

    public List<int> GetRestaurantIdsByOrderNumber()
    {
        return _orderRepository.GetRestaurantIdsByOrderNumber();
    }

    public void PlaceOrder(string userId, PromoCode? promo)
    {
        var userShoppingCart = _shoppingCartService.GetCartByUserId(userId);
        var userData = _userDataService.GetUserData(userId);

        if (userData == null)
        {
            return;
        }

        var groupedItems = userShoppingCart
            .GroupBy(item => item.Product.RestaurantId)
            .ToDictionary(group => group.Key, group => group.ToList());

        foreach (var (restaurantId, items) in groupedItems)
        {
            var orderItems = _mapper.Map<List<OrderItem>>(items);

            var order = new Order
            {
                UserId = userId,
                IsDone = false,
                OrdererAddress = userData.Address,
                OrdererName = userData.Name,
                OrdererPhoneNumber = userData.Phone,
                OrderItems = orderItems,
                RestaurantId = restaurantId,
                PromoCodeId = promo?.Id
            };

            _orderRepository.PlaceOrder(order);
        }

        _shoppingCartService.ClearCart(userId);
    }

    public void UpdateOrder(int orderId)
    {
        var order = _orderRepository.GetOrderById(orderId);

        if (order == null)
        {
            return;
        }

        order.IsDone = !order.IsDone;

        _orderRepository.UpdateOrder(order);
    }

    public void DeleteOrder()
    {
    }
}