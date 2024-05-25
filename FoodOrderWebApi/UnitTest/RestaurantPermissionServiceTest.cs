using AutoMapper;
using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.Order;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services;
using FoodOrderWebApi.Services.Interfaces;

namespace UnitTest;

public class RestaurantPermissionServiceTest : IDisposable
{
    private readonly FoodOrderDbContext _context;
    private readonly RestaurantPermissionService _service;

    public RestaurantPermissionServiceTest()
    {
        var options = DbContextOptions.GetOptions("TestDatabase_OrderService");
        _context = new FoodOrderDbContext(options);

        var permissionRepository = new RestaurantPermissionRepository(_context);
        var keycloakService = new KeycloakService();

        _service = new RestaurantPermissionService(permissionRepository, keycloakService);

        DbContextInitializer.InitializeForTestDb(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public void GetAllPermissionByUserIdTest()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";

        var result = _service.GetAllPermissionByUserId(userId);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetPermissionByUserIdAndRestaurantIdTest()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        const int restaurantIdTrue = 1;
        const int restaurantIdFalse = 10;

        var hasPermission = _service.GetPermissionByUserIdAndRestaurantId(userId, restaurantIdTrue);
        var noPermission = _service.GetPermissionByUserIdAndRestaurantId(userId, restaurantIdFalse);

        Assert.True(hasPermission);
        Assert.False(noPermission);
    }

    [Fact]
    public void AddPermissionToUser()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var permission = new RestaurantPermission { UserId = userId, RestaurantId = 10 };

        _service.AddPermissionToUser(userId, permission);
        var hasPermission = _service.GetPermissionByUserIdAndRestaurantId(userId, 10);

        Assert.True(hasPermission);
    }
}