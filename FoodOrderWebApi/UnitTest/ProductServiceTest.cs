using AutoMapper;
using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs.CreateProduct;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace UnitTest;

public class ProductServiceTest : IDisposable
{
    private FoodOrderDbContext _context;
    private ProductService _service;

    public ProductServiceTest()
    {
        _context = new FoodOrderDbContext(DbContextOptions.GetOptions("TestDatabase_ProductService"));

        var config = new MapperConfiguration(ConfigureMapper);
        var mapper = config.CreateMapper();

        var productRepository = new ProductRepository(_context);
        var foodCategoryRepository = new FoodCategoryRepository(_context);
        var assetService = new FakeAssetService();

        _service = new ProductService(productRepository, foodCategoryRepository, mapper, assetService);

        DbContextInitializer.InitializeForTestDb(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private void ConfigureMapper(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<CreateEditProductDto, Product>()
            .ForMember(dest => dest.PictureName, opt => opt.MapFrom((src, dest) =>
                src.Picture != null ? src.Picture.FileName : "nothing"));

        cfg.CreateMap<Product, CreateEditProductDto>()
            .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));
    }

    [Fact]
    public async Task CreateProductTest()
    {
        const int restaurantId = 1;
        var createEditProductDto = new CreateEditProductDto
        {
            Name = "New Product",
            Price = 2000,
            RestaurantId = restaurantId,
            CategoryNames = new List<string> { "Starters" },
            Description = "description",
            Picture = null,
        };

        var restaurantNumberOfProductsBeforeProductCreation =
            _context.Products.Count(p => p.RestaurantId == restaurantId);

        var result = await _service.CreateProduct(createEditProductDto);

        var restaurantNumberOfProductsAfterProductCreation =
            _context.Products.Count(p => p.RestaurantId == restaurantId);

        Assert.Equal(restaurantNumberOfProductsBeforeProductCreation + 1,
            restaurantNumberOfProductsAfterProductCreation);
    }

    [Fact]
    public async Task EditProductTest_ExistingProduct_ChangesDetected()
    {
        const int restaurantId = 1;
        const int productId = 1;
        var createEditProductDto = new CreateEditProductDto
        {
            Id = productId,
            Name = "Updated Product",
            Price = 2000,
            RestaurantId = restaurantId,
            CategoryNames = new List<string> { "Salad" },
            Description = "Updated desc",
            Picture = null,
        };

        var product = _context.Products
            .Include(product => product.Categories)
            .First(p => p.Id == productId);

        Assert.NotEqual(createEditProductDto.Name, product.Name);
        Assert.NotEqual(createEditProductDto.Description, product.Description);
        Assert.NotEqual(createEditProductDto.CategoryNames, product.Categories.Select(c => c.Name).ToList());

        await _service.EditProduct(createEditProductDto);

        Assert.Equal(createEditProductDto.Name, product.Name);
        Assert.Equal(createEditProductDto.Description, product.Description);
        Assert.Equal(createEditProductDto.CategoryNames, product.Categories.Select(c => c.Name).ToList());
    }
}