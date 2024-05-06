using System.Security.Claims;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrderWebApi.Controllers;

[Route("favourite")]
[ApiController]
public class FavouriteRestaurantController : Controller
{
    private readonly IFavouriteRestaurantService _favouriteRestaurantService;

    public FavouriteRestaurantController(IFavouriteRestaurantService favouriteRestaurantService)
    {
        _favouriteRestaurantService = favouriteRestaurantService;
    }

    [HttpGet]
    public List<RestaurantDto> GetAllFavouriteRestaurant()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return userId.IsNullOrEmpty() ? null : _favouriteRestaurantService.GetAllFavouriteRestaurant(userId);
    }

    [HttpGet("{restaurantId}")]
    public bool GetIsRestaurantInFavourites(int restaurantId)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return userId.IsNullOrEmpty()
            ? false
            : _favouriteRestaurantService.GetIsRestaurantInFavourites(userId, restaurantId);
    }

    [HttpPost("change-state")]
    public IActionResult ChangeStateOfFavouriteRestaurant([FromBody] FavouriteRequestDto favouriteRequestDto)
    {
        var a = HttpContext.User.FindAll(ClaimTypes.UserData).ToList();
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId.IsNullOrEmpty())
        {
            return Unauthorized();
        }

        _favouriteRestaurantService.ChangeStateOfFavouriteRestaurant(userId, favouriteRequestDto.RestaurantId);

        return Ok();
    }
}