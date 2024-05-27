using AutoMapper;
using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.Order;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Services;

namespace UnitTest;

public class UserDataServiceTest : IDisposable
{
    private readonly FoodOrderDbContext _context;
    private readonly UserDataService _service;

    public UserDataServiceTest()
    {
        var options = DbContextOptions.GetOptions("TestDatabase_OrderService");
        _context = new FoodOrderDbContext(options);

        var config = new MapperConfiguration(ConfigureMapper);
        var mapper = config.CreateMapper();

        var userDataRepository = new UserDataRepository(_context);
        var keycloakService = new KeycloakService();

        _service = new UserDataService(userDataRepository, keycloakService, mapper);

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
    public void GetUserData()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var userData = _service.GetUserData(userId);

        Assert.NotNull(userData);
        Assert.Equal("Levente Smid", userData.Name);
        Assert.Equal("levente3000123@gmail.com", userData.Email);
    }
    
    [Fact]
    public void GetUserHasData()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var hasData = _service.GetUserHasData(userId);

        Assert.False(hasData);
    }
    
    [Fact]
    public void UpdateUserData_NewUser_CreatesUserData()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var updateDto = new UpdateUserDataDto { Name = "Levente Smid", Address = "Wesselényi 61", Email = "levente3000123@gmail.com", Phone = "+36301234567" };

        _service.UpdateUserData(userId, updateDto);

        var userData = _context.UserData.FirstOrDefault(u => u.UserId == userId);

        Assert.NotNull(userData);
        Assert.Equal("Wesselényi 61", userData.Address);
        Assert.Equal("+36301234567", userData.Phone);
    }
}