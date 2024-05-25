using AutoMapper;
using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.Order;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Services;

namespace UnitTest;

public class OrderServiceTest : IDisposable
{
    private readonly FoodOrderDbContext _context;
    private readonly OrderService _service;
    private readonly ShoppingCartRepository _shoppingCartRepository;
    private readonly UserDataRepository _userDataRepository;

    public OrderServiceTest()
    {
        var options = DbContextOptions.GetOptions("TestDatabase_OrderService");
        _context = new FoodOrderDbContext(options);

        var config = new MapperConfiguration(ConfigureMapper);
        var mapper = config.CreateMapper();

        var orderRepository = new OrderRepository(_context);
        _shoppingCartRepository = new ShoppingCartRepository(_context);
        _userDataRepository = new UserDataRepository(_context);

        var keycloakService = new KeycloakService();
        var shoppingCartService = new ShoppingCartService(_shoppingCartRepository, mapper);
        var userDataService = new UserDataService(_userDataRepository, keycloakService, mapper);

        _service = new OrderService(orderRepository, shoppingCartService, userDataService, mapper);

        DbContextInitializer.InitializeForTestDb(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private void ConfigureMapper(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Order, OrderDto>();
        cfg.CreateMap<OrderItem, OrderItemDto>();

        cfg.CreateMap<UpdateUserDataDto, UserData>();

        cfg.CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));

        cfg.CreateMap<ShoppingCartItem, ShoppingCartItemDto>();

        cfg.CreateMap<ShoppingCartItemDto, OrderItem>()
            .ForMember(dest => dest.Product, opt => opt.Ignore());
    }

    [Fact]
    public void PlaceOrderTest_WithInvalidUserId()
    {
        const string userId = "test-user";
        var exception = Record.Exception(() => _service.PlaceOrder(userId, null));

        Assert.NotNull(exception);
        Assert.IsType<AggregateException>(exception);
    }

    [Fact]
    public void PlaceOrderTest_WithValidUserId()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        const int productId1 = 1;
        const int productId2 = 10;
        const int quantity1 = 2;
        const int quantity2 = 4;
        _shoppingCartRepository.AddProduct(userId, productId1, quantity1);
        _shoppingCartRepository.AddProduct(userId, productId2, quantity2);
        _userDataRepository.CreateUserData(new UserData
        {
            UserId = userId,
            Name = "Levente Smid",
            Address = "Wesselényi 61",
            Email = "levente3000123@gmail.com",
            Phone = "+36301234567"
        });
        _context.SaveChanges();
        _service.PlaceOrder(userId, null);

        var numberOfShoppingCartItems = _context.ShoppingCartItems.Count(o => o.UserId == userId);
        var numberOfOrderItems = _context.OrderItems.Count(orderItem =>
            orderItem.ProductId == productId1 && orderItem.Quantity == quantity1 ||
            orderItem.ProductId == productId2 && orderItem.Quantity == quantity2);
        Assert.Equal(0, numberOfShoppingCartItems);
        Assert.Equal(2, numberOfOrderItems);
    }

    [Fact]
    public void UpdateOrderTest()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        const int productId1 = 1;
        const int productId2 = 10;
        const int quantity1 = 2;
        const int quantity2 = 4;
        _shoppingCartRepository.AddProduct(userId, productId1, quantity1);
        _shoppingCartRepository.AddProduct(userId, productId2, quantity2);
        _userDataRepository.CreateUserData(new UserData
        {
            UserId = userId,
            Name = "Levente Smid",
            Address = "Wesselényi 61",
            Email = "levente3000123@gmail.com",
            Phone = "+36301234567"
        });
        _context.SaveChanges();
        _service.PlaceOrder(userId, null);

        var order = _context.Orders.First(order => order.UserId == userId);

        _service.UpdateOrder(order.Id);
        Assert.True(order.IsDone);

        _service.UpdateOrder(order.Id);
        Assert.False(order.IsDone);
    }

    [Fact]
    public void GetActiveOrdersByRestaurantIdTest()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        const int productId1 = 1;
        const int productId2 = 10;
        const int quantity1 = 2;
        const int quantity2 = 4;
        _shoppingCartRepository.AddProduct(userId, productId1, quantity1);
        _shoppingCartRepository.AddProduct(userId, productId2, quantity2);
        _userDataRepository.CreateUserData(new UserData
        {
            UserId = userId,
            Name = "Levente Smid",
            Address = "Wesselényi 61",
            Email = "levente3000123@gmail.com",
            Phone = "+36301234567"
        });
        _context.SaveChanges();
        _service.PlaceOrder(userId, null);

        var order = _context.Orders.First(order => order.UserId == userId);

        var result = _service.GetActiveOrdersByRestaurantId(order.RestaurantId);
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Single(result.First().OrderItems);
    }

    [Fact]
    public void GetDoneOrdersByRestaurantIdTest_()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        const int productId1 = 1;
        const int productId2 = 10;
        const int quantity1 = 2;
        const int quantity2 = 4;
        _shoppingCartRepository.AddProduct(userId, productId1, quantity1);
        _shoppingCartRepository.AddProduct(userId, productId2, quantity2);
        _userDataRepository.CreateUserData(new UserData
        {
            UserId = userId,
            Name = "Levente Smid",
            Address = "Wesselényi 61",
            Email = "levente3000123@gmail.com",
            Phone = "+36301234567"
        });
        _context.SaveChanges();
        _service.PlaceOrder(userId, null);

        var order = _context.Orders.First(order => order.UserId == userId);
        _service.UpdateOrder(order.Id);

        var result = _service.GetDoneOrdersByRestaurantId(order.RestaurantId);
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Single(result.First().OrderItems);
    }
}