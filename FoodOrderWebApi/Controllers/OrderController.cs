using System.Security.Claims;
using FoodOrderWebApi.DTOs.Order;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrderWebApi.Controllers;

[Route("order")]
[ApiController]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("active/{restaurantId}")]
    public ActionResult<List<OrderDto>> GetActiveOrdersByRestaurantId(int restaurantId)
    {
        return Ok(_orderService.GetActiveOrdersByRestaurantId(restaurantId));
    }

    [HttpGet("done/{restaurantId}")]
    public ActionResult<List<OrderDto>> GetDoneOrdersByRestaurantId(int restaurantId)
    {
        return Ok(_orderService.GetDoneOrdersByRestaurantId(restaurantId));
    }

    [HttpPost("place-order")]
    public ActionResult PlaceOrder([FromBody] PromoCode? promo)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId.IsNullOrEmpty())
        {
            return Unauthorized();
        }

        _orderService.PlaceOrder(userId, promo);
        return Ok();
    }

    [HttpPost("update-order")]
    public ActionResult UpdateOrder([FromBody] int orderId)
    {
        _orderService.UpdateOrder(orderId);
        return Ok();
    }
}