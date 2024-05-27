using AutoMapper;
using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.CreateProduct;
using FoodOrderWebApi.DTOs.ShoppingCart;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Services;

namespace UnitTest;

public class ShoppingCartServiceTest : IDisposable
{
    private FoodOrderDbContext _context;
    private ShoppingCartService _service;

    public ShoppingCartServiceTest()
    {
        _context = new FoodOrderDbContext(DbContextOptions.GetOptions("TestDatabase_ProductService"));

        var config = new MapperConfiguration(ConfigureMapper);
        var mapper = config.CreateMapper();

        var shoppingCartRepository = new ShoppingCartRepository(_context);

        _service = new ShoppingCartService(shoppingCartRepository, mapper);

        DbContextInitializer.InitializeForTestDb(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private void ConfigureMapper(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Product, ProductDto>();
        cfg.CreateMap<ShoppingCartItem, ShoppingCartItemDto>();
    }

    [Fact]
    public void AddProduct()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var newProduct = new ShoppingCartProductDto { ProductId = 1, Quantity = 2 };

        _service.AddProduct(userId, newProduct);

        var result =
            _context.ShoppingCartItems.FirstOrDefault(item =>
                item.UserId == userId && item.ProductId == newProduct.ProductId);

        Assert.NotNull(result);
        Assert.Equal(newProduct.Quantity, result.Quantity);
    }

    [Fact]
    public void UpdateQuantity()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var newProduct = new ShoppingCartProductDto { ProductId = 1, Quantity = 2 };

        _service.AddProduct(userId, newProduct);

        var cartItem = _context.ShoppingCartItems.First();
        var updateDto = new UpdateItemQuantityDto { ShoppingCartItemId = cartItem.ShoppingCartItemId, Quantity = 5 };

        _service.UpdateQuantity(updateDto);

        var updatedItem = _context.ShoppingCartItems.Find(cartItem.ShoppingCartItemId);
        Assert.Equal(updateDto.Quantity, updatedItem.Quantity);
    }

    [Fact]
    public void RemoveProduct()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var newProduct = new ShoppingCartProductDto { ProductId = 1, Quantity = 2 };

        _service.AddProduct(userId, newProduct);

        var cartItem = _context.ShoppingCartItems.First();

        _service.RemoveProduct(cartItem.ShoppingCartItemId);

        var result = _context.ShoppingCartItems.Find(cartItem.ShoppingCartItemId);
        Assert.Null(result);
    }

    [Fact]
    public void GetCartByUserId()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var newProduct1 = new ShoppingCartProductDto { ProductId = 1, Quantity = 2 };
        var newProduct2 = new ShoppingCartProductDto { ProductId = 2, Quantity = 2 };

        _service.AddProduct(userId, newProduct1);
        _service.AddProduct(userId, newProduct2);

        var result = _service.GetCartByUserId(userId);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void ClearCart()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var newProduct1 = new ShoppingCartProductDto { ProductId = 1, Quantity = 2 };
        var newProduct2 = new ShoppingCartProductDto { ProductId = 2, Quantity = 2 };

        _service.AddProduct(userId, newProduct1);
        _service.AddProduct(userId, newProduct2);

        var resultBefore = _context.ShoppingCartItems.Count(item => item.UserId == userId);

        _service.ClearCart(userId);

        var resultAfter = _context.ShoppingCartItems.Count(item => item.UserId == userId);
        Assert.Equal(resultAfter + 2, resultBefore);
    }
}