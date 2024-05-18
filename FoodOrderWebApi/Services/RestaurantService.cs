using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.CreateRestaurant;
using FoodOrderWebApi.Enum;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services.Interfaces;

namespace FoodOrderWebApi.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IOpeningHourRepository _openingHourRepository;
    private readonly IRestaurantPermissionService _restaurantPermissionService;
    private readonly IProductRepository _productRepository;
    private readonly IOrderService _orderService;
    private readonly AssetsService _assetService;
    private readonly IMapper _mapper;
    private readonly string DIRECTORY = "restaurant";

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, AssetsService assetService,
        IOpeningHourRepository openingHourRepository, IRestaurantPermissionService restaurantPermissionService,
        IProductRepository productRepository, IOrderService orderService)
    {
        _restaurantRepository = restaurantRepository;
        _openingHourRepository = openingHourRepository;
        _restaurantPermissionService = restaurantPermissionService;
        _productRepository = productRepository;
        _orderService = orderService;
        _assetService = assetService;
        _mapper = mapper;
    }

    public List<RestaurantDto> GetAllRestaurantsWithProductsAndCategories()
    {
        return _mapper.Map<List<RestaurantDto>>(_restaurantRepository.GetAll());
    }

    public List<RestaurantDto> GetLatestRestaurants()
    {
        var restaurants = _mapper.Map<List<RestaurantDto>>(_restaurantRepository.GetLatestRestaurants());
        foreach (var restaurant in restaurants)
        {
            restaurant.PriceCategory = _productRepository.GetIfAnyProductUnderRestaurant(restaurant.Id)
                ? DeterminePriceCategory(_productRepository.GetAveragePriceByRestaurantId(restaurant.Id))
                : DeterminePriceCategory(0);
        }

        return restaurants;
    }

    public List<RestaurantDto> GetRestaurantsWithTheMostOrders()
    {
        var restaurants = _mapper.Map<List<RestaurantDto>>(
            _restaurantRepository.GetRestaurantsWithTheMostOrders(_orderService.GetRestaurantIdsByOrderNumber()));

        foreach (var restaurant in restaurants)
        {
            restaurant.PriceCategory = _productRepository.GetIfAnyProductUnderRestaurant(restaurant.Id)
                ? DeterminePriceCategory(_productRepository.GetAveragePriceByRestaurantId(restaurant.Id))
                : DeterminePriceCategory(0);
        }

        return restaurants;
    }

    public List<RestaurantDto> GetRestaurantsByCategory(string categoryName)
    {
        return _mapper.Map<List<RestaurantDto>>(
            _restaurantRepository.GetRestaurantsByCategory(categoryName));
    }

    public string? GetRestaurantNameById(int restaurantId)
    {
        return _restaurantRepository.GetRestaurantNameById(restaurantId);
    }

    public RestaurantDetailsDto GetRestaurantByIdWithProductsAndCategories(int id)
    {
        var restaurant = _mapper.Map<RestaurantDetailsDto>(_restaurantRepository.GetByIdOrName(id));
        restaurant.CategoriesWithProducts =
            _mapper.Map<ICollection<ProductsInCategoryDto>>(_restaurantRepository
                .GetRestaurantFoodCategoriesWithProducts(id));

        var productsWithoutCategory = _mapper.Map<IList<ProductDto>>(_productRepository
            .GetProductWithoutCategoryByRestaurantId(id));
        if (productsWithoutCategory.Count != 0)
        {
            restaurant.CategoriesWithProducts.Add(new ProductsInCategoryDto
            {
                Name = "Without Category",
                Products = productsWithoutCategory
            });
        }

        return restaurant;
    }

    public CreateEditRestaurantDto GetRestaurantByIdForEdit(int id)
    {
        var restaurant = _restaurantRepository.GetByIdOrName(id);
        var a = _mapper.Map<CreateEditRestaurantDto>(restaurant);
        return a;
    }

    public async Task<int> CreateRestaurant(CreateEditRestaurantDto createEditRestaurant, string userId)
    {
        if (createEditRestaurant.Logo != null && createEditRestaurant.Banner != null)
        {
            await _assetService.SaveAssetIfNotExists(createEditRestaurant.Logo, DIRECTORY);
            await _assetService.SaveAssetIfNotExists(createEditRestaurant.Banner, DIRECTORY);
        }

        var openingHours = _mapper.Map<OpeningHour>(createEditRestaurant.OpeningHours);
        var closingHours = _mapper.Map<OpeningHour>(createEditRestaurant.ClosingHours);

        _openingHourRepository.CreateOpeningHour(openingHours);
        _openingHourRepository.CreateOpeningHour(closingHours);

        var restaurant = _mapper.Map<Restaurant>(createEditRestaurant);
        restaurant.OpeningHours = openingHours;
        restaurant.ClosingHours = closingHours;

        _restaurantRepository.CreateRestaurant(restaurant);

        var restaurantPermission = new RestaurantPermission
        {
            UserId = userId,
            RestaurantId = restaurant.Id
        };

        _restaurantPermissionService.AddPermissionToUser(userId, restaurantPermission);

        return restaurant.Id;
    }

    public async Task<int?> EditRestaurant(CreateEditRestaurantDto createEditRestaurant)
    {
        Restaurant? restaurant = null;
        if (createEditRestaurant.Id.HasValue)
        {
            restaurant = _restaurantRepository.GetByIdAsTracking(createEditRestaurant.Id.Value);
        }

        if (restaurant == null)
        {
            return null;
        }

        if (createEditRestaurant.Logo?.FileName != null)
        {
            await _assetService.SaveAssetIfNotExists(createEditRestaurant.Logo, DIRECTORY);
        }

        if (createEditRestaurant.Banner?.FileName != null)
        {
            await _assetService.SaveAssetIfNotExists(createEditRestaurant.Banner, DIRECTORY);
        }

        var openingHoursToRemove = restaurant.OpeningHours;
        var closingHoursToRemove = restaurant.ClosingHours;

        _mapper.Map(createEditRestaurant, restaurant);

        var openingHours = _mapper.Map<OpeningHour>(createEditRestaurant.OpeningHours);
        var closingHours = _mapper.Map<OpeningHour>(createEditRestaurant.ClosingHours);

        _openingHourRepository.CreateOpeningHour(openingHours);
        _openingHourRepository.CreateOpeningHour(closingHours);

        restaurant.OpeningHourId = openingHours.Id;
        restaurant.ClosingHourId = closingHours.Id;

        _restaurantRepository.UpdateRestaurant(restaurant);

        _openingHourRepository.RemoveOpeningHour(openingHoursToRemove);
        _openingHourRepository.RemoveOpeningHour(closingHoursToRemove);

        return restaurant.Id;
    }

    private static PriceCategory DeterminePriceCategory(double averagePrice)
    {
        if (averagePrice == 0)
        {
            return PriceCategory.NoProduct;
        }

        return averagePrice switch
        {
            <= 1500 => PriceCategory.Low,
            <= 4000 => PriceCategory.Medium,
            _ => PriceCategory.High
        };
    }
}