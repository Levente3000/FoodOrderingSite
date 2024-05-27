using AutoMapper;
using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.CreateProduct;
using FoodOrderWebApi.DTOs.CreateRestaurant;
using FoodOrderWebApi.DTOs.Order;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Services;
using NodaTime;
using NodaTime.Text;

namespace UnitTest;

public class RestaurantServiceTest : IDisposable
{
    private readonly FoodOrderDbContext _context;
    private readonly RestaurantService _service;

    public RestaurantServiceTest()
    {
        _context = new FoodOrderDbContext(DbContextOptions.GetOptions("TestDatabase_RestaurantService"));

        var config = new MapperConfiguration(ConfigureMapper);
        var mapper = config.CreateMapper();

        var restaurantRepository = new RestaurantRepository(_context);
        var openingHourRepository = new OpeningHourRepository(_context);
        var productRepository = new ProductRepository(_context);
        var restaurantPermissionRepository = new RestaurantPermissionRepository(_context);
        var orderRepository = new OrderRepository(_context);
        var shoppingCartRepository = new ShoppingCartRepository(_context);
        var userDataRepository = new UserDataRepository(_context);

        var keycloakService = new KeycloakService();
        var shoppingCartService = new ShoppingCartService(shoppingCartRepository, mapper);
        var userDataService = new UserDataService(userDataRepository, keycloakService, mapper);
        var restaurantPermissionService =
            new RestaurantPermissionService(restaurantPermissionRepository, keycloakService);
        var orderService = new OrderService(orderRepository, shoppingCartService, userDataService, mapper);
        var assetService = new FakeAssetService();

        _service = new RestaurantService(restaurantRepository, mapper, assetService, openingHourRepository,
            restaurantPermissionService, productRepository, orderService);

        DbContextInitializer.InitializeForTestDb(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private void ConfigureMapper(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dto => dto.CreatedAt, conf => conf.MapFrom(oh => FormatInstant(oh.CreatedAt)));

        cfg.CreateMap<Restaurant, RestaurantDetailsDto>()
            .ForMember(dto => dto.CreatedAt, conf => conf.MapFrom(oh => FormatInstant(oh.CreatedAt)));

        cfg.CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));
        cfg.CreateMap<ProductDto, Product>();

        cfg.CreateMap<FoodCategory, FoodCategoryDto>();
        cfg.CreateMap<FoodCategory, ProductsInCategoryDto>();

        cfg.CreateMap<OpeningHour, OpeningHourDto>()
            .ForMember(dto => dto.Monday, conf => conf.MapFrom(oh => FormatInstant(oh.Monday)))
            .ForMember(dto => dto.Tuesday, conf => conf.MapFrom(oh => FormatInstant(oh.Tuesday)))
            .ForMember(dto => dto.Wednesday, conf => conf.MapFrom(oh => FormatInstant(oh.Wednesday)))
            .ForMember(dto => dto.Thursday, conf => conf.MapFrom(oh => FormatInstant(oh.Thursday)))
            .ForMember(dto => dto.Friday, conf => conf.MapFrom(oh => FormatInstant(oh.Friday)))
            .ForMember(dto => dto.Saturday, conf => conf.MapFrom(oh => FormatInstant(oh.Saturday)))
            .ForMember(dto => dto.Sunday, conf => conf.MapFrom(oh => FormatInstant(oh.Sunday)));

        cfg.CreateMap<CreateRestaurantOpeningHoursDto, OpeningHour>()
            .ForMember(dto => dto.Monday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Monday)))
            .ForMember(dto => dto.Tuesday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Tuesday)))
            .ForMember(dto => dto.Wednesday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Wednesday)))
            .ForMember(dto => dto.Thursday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Thursday)))
            .ForMember(dto => dto.Friday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Friday)))
            .ForMember(dto => dto.Saturday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Saturday)))
            .ForMember(dto => dto.Sunday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Sunday)));

        cfg.CreateMap<ShoppingCartItem, ShoppingCartItemDto>();
        cfg.CreateMap<ShoppingCartItemDto, ShoppingCartItem>();

        cfg.CreateMap<CreateEditRestaurantDto, Restaurant>()
            .ForMember(dest => dest.LogoName, opt => opt.MapFrom((src, dest) =>
                src.Logo != null ? src.Logo.FileName : "nothing"))
            .ForMember(dest => dest.BannerName, opt => opt.MapFrom((src, dest) =>
                src.Banner != null ? src.Banner.FileName : "nothing"))
            .ForMember(dest => dest.OpeningHours, opt => opt.MapFrom(src => MapOpeningHours(src.OpeningHours)))
            .ForMember(dest => dest.ClosingHours, opt => opt.MapFrom(src => MapOpeningHours(src.ClosingHours)));

        cfg.CreateMap<Restaurant, CreateEditRestaurantDto>()
            .ForMember(dest => dest.OpeningHours,
                opt => opt.MapFrom(src => MapOpeningHoursFromInstantToString(src.OpeningHours)))
            .ForMember(dest => dest.ClosingHours,
                opt => opt.MapFrom(src => MapOpeningHoursFromInstantToString(src.ClosingHours)));
    }

    private static string? FormatInstant(Instant? instant) =>
        instant?.ToDateTimeOffset().ToString("o");

    private static OpeningHour MapOpeningHours(CreateRestaurantOpeningHoursDto dto)
    {
        return new OpeningHour
        {
            Monday = ConvertTimeToInstant(dto.Monday),
            Tuesday = ConvertTimeToInstant(dto.Tuesday),
            Wednesday = ConvertTimeToInstant(dto.Wednesday),
            Thursday = ConvertTimeToInstant(dto.Thursday),
            Friday = ConvertTimeToInstant(dto.Friday),
            Saturday = ConvertTimeToInstant(dto.Saturday),
            Sunday = ConvertTimeToInstant(dto.Sunday)
        };
    }

    private static Instant? ConvertTimeToInstant(string? time)
    {
        if (time is "null" or null)
        {
            return null;
        }

        var pattern = LocalTimePattern.CreateWithInvariantCulture("h:mm tt");
        var parseResult = pattern.Parse(time);

        if (!parseResult.Success)
        {
            throw new ArgumentException($"Invalid time format: {time}");
        }

        var referenceDate = new LocalDate(2000, 1, 1);
        var dateTime = referenceDate.At(parseResult.Value);

        return dateTime.InUtc().ToInstant();
    }

    private static CreateRestaurantOpeningHoursDto MapOpeningHoursFromInstantToString(OpeningHour openingHour)
    {
        return new CreateRestaurantOpeningHoursDto
        {
            Monday = FormatTimeFromInstantToString(openingHour.Monday),
            Tuesday = FormatTimeFromInstantToString(openingHour.Tuesday),
            Wednesday = FormatTimeFromInstantToString(openingHour.Wednesday),
            Thursday = FormatTimeFromInstantToString(openingHour.Thursday),
            Friday = FormatTimeFromInstantToString(openingHour.Friday),
            Saturday = FormatTimeFromInstantToString(openingHour.Saturday),
            Sunday = FormatTimeFromInstantToString(openingHour.Sunday)
        };
    }

    private static string FormatTimeFromInstantToString(Instant? instant)
    {
        if (!instant.HasValue)
        {
            return string.Empty;
        }

        LocalDateTime localDateTime = instant.Value.InUtc().LocalDateTime;
        var pattern = LocalTimePattern.CreateWithInvariantCulture("hh:mm tt");
        return pattern.Format(localDateTime.TimeOfDay);
    }

    [Fact]
    public void GetAllRestaurantsWithProductsAndCategoriesTest()
    {
        var restaurants = _service.GetAllRestaurantsWithProductsAndCategories();
        var expectedCount = _context.Restaurants.Count();

        Assert.Equal(expectedCount, restaurants.Count);
    }

    [Fact]
    public void GetLatestRestaurantsTest()
    {
        var latestRestaurants = _service.GetLatestRestaurants();
        Assert.NotNull(latestRestaurants);
        Assert.True(latestRestaurants.Count <= 10);
    }

    [Fact]
    public void GetRestaurantsWithTheMostOrdersTest()
    {
        var restaurants = _service.GetRestaurantsWithTheMostOrders();
        Assert.NotNull(restaurants);
        Assert.Empty(restaurants);
    }

    [Fact]
    public void GetRestaurantByIdWithProductsAndCategoriesTest()
    {
        const int restaurantId = 1;
        var restaurantDetails = _service.GetRestaurantByIdWithProductsAndCategories(restaurantId);

        Assert.NotNull(restaurantDetails);
        Assert.NotNull(restaurantDetails.CategoriesWithProducts);
        Assert.Equal(2, restaurantDetails.CategoriesWithProducts.Count);
    }

    [Fact]
    public async Task EditRestaurantTest()
    {
        var restaurant = _context.Restaurants.First();
        var editDto = new CreateEditRestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Description = "test desc",
            Address = restaurant.Address,
            PhoneNumber = restaurant.PhoneNumber,
            Banner = null,
            Logo = null,
            OpeningHours = new CreateRestaurantOpeningHoursDto(),
            ClosingHours = new CreateRestaurantOpeningHoursDto()
        };

        var result = await _service.EditRestaurant(editDto);

        Assert.NotNull(result);
        Assert.Equal("test desc", _context.Restaurants.Find(result.Value).Description);
    }

    [Fact]
    public async Task CreateRestaurantTest()
    {
        const string userId = "e79a7435-8c69-469c-b831-66aad159aa33";
        var createRestaurantDto = new CreateEditRestaurantDto
        {
            Name = "New Restaurant",
            Description = "A new test restaurant",
            Logo = null,
            Banner = null,
            Address = "test address",
            PhoneNumber = "+3630123567",
            OpeningHours = new CreateRestaurantOpeningHoursDto(),
            ClosingHours = new CreateRestaurantOpeningHoursDto()
        };

        var restaurantCountBefore = _context.Restaurants.Count();

        var newRestaurantId = await _service.CreateRestaurant(createRestaurantDto, userId);

        var restaurantCountAfter = _context.Restaurants.Count();
        Assert.Equal(restaurantCountBefore + 1, restaurantCountAfter);
        Assert.True(newRestaurantId > 0);
    }
}