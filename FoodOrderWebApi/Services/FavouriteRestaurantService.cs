using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Enum;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services.Interfaces;

namespace FoodOrderWebApi.Services;

public class FavouriteRestaurantService : IFavouriteRestaurantService
{
    private readonly IFavouriteRestaurantRepository _favouriteRestaurantRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public FavouriteRestaurantService(IProductRepository productRepository,
        IFavouriteRestaurantRepository favouriteRestaurantRepository, IMapper mapper)
    {
        _favouriteRestaurantRepository = favouriteRestaurantRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public List<RestaurantDto> GetAllFavouriteRestaurant(string userId)
    {
        var favouriteRestaurants =
            _favouriteRestaurantRepository.GetAllFavouriteRestaurant(userId)
                .Select(fR => fR.Restaurant)
                .ToList();
        foreach (var restaurant in favouriteRestaurants)
        {
            restaurant.PriceCategory = _productRepository.GetIfAnyProductUnderRestaurant(restaurant.Id)
                ? DeterminePriceCategory(_productRepository.GetAveragePriceByRestaurantId(restaurant.Id))
                : DeterminePriceCategory(0);
        }

        return _mapper.Map<List<RestaurantDto>>(favouriteRestaurants);
    }

    public bool GetIsRestaurantInFavourites(string userId, int restaurantId)
    {
        var favouriteRestaurant =
            _favouriteRestaurantRepository.GetFavouriteRestaurantByUserIdAndRestaurantId(userId, restaurantId);

        return favouriteRestaurant != null;
    }

    public void ChangeStateOfFavouriteRestaurant(string userId, int restaurantId)
    {
        var favRestaurant =
            _favouriteRestaurantRepository.GetFavouriteRestaurantByUserIdAndRestaurantId(userId, restaurantId);

        if (favRestaurant == null)
        {
            _favouriteRestaurantRepository.AddFavouriteRestaurant(new FavouriteRestaurant
            {
                userId = userId,
                RestaurantId = restaurantId,
            });
        }
        else
        {
            _favouriteRestaurantRepository.RemoveFavouriteRestaurant(favRestaurant);
        }
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