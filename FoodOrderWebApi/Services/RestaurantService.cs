using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.CreateRestaurant;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using NodaTime.Text;

namespace FoodOrderWebApi.Services;

public class RestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IOpeningHourRepository _openingHourRepository;
    private readonly IRestaurantPermissionRepository _restaurantPermissionRepository;
    private readonly IProductRepository _productRepository;
    private readonly AssetsService _assetService;
    private readonly IMapper _mapper;

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, AssetsService assetService,
        IOpeningHourRepository openingHourRepository, IRestaurantPermissionRepository restaurantPermissionRepository,
        IProductRepository productRepository)
    {
        _restaurantRepository = restaurantRepository;
        _openingHourRepository = openingHourRepository;
        _restaurantPermissionRepository = restaurantPermissionRepository;
        _productRepository = productRepository;
        _assetService = assetService;
        _mapper = mapper;
    }

    public List<RestaurantDto> GetAllRestaurantsWithProductsAndCategories()
    {
        var restaurants = _restaurantRepository.GetAll();
        return _mapper.Map<List<RestaurantDto>>(restaurants);
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

    public int CreateRestaurant(CreateEditRestaurantDto createEditRestaurant, string userId)
    {
        if (createEditRestaurant.Logo != null && createEditRestaurant.Banner != null)
        {
            _assetService.SaveAssetToRestaurantDictionary(createEditRestaurant.Logo);
            _assetService.SaveAssetToRestaurantDictionary(createEditRestaurant.Banner);
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

        _restaurantPermissionRepository.AddPermissionToUser(restaurantPermission);

        return restaurant.Id;
    }

    public int? EditRestaurant(CreateEditRestaurantDto createEditRestaurant)
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
}