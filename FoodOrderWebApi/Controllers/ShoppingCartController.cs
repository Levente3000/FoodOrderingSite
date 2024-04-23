using System.Security.Claims;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.ShoppingCart;
using FoodOrderWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrderWebApi.Controllers;

[Route("shopping-cart")]
[ApiController]
public class ShoppingCartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet]
    public ActionResult<List<ShoppingCartItemDto>> GetShoppingCart()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return userId.IsNullOrEmpty()
            ? Unauthorized("User ID is null or empty.")
            : Ok(_shoppingCartService.GetCartByUserId(userId));
    }

    [HttpPost("add-product")]
    public IActionResult AddProductToShoppingCart([FromBody] ShoppingCartProductDto shoppingCartProduct)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId.IsNullOrEmpty())
        {
            return Unauthorized("User ID is null or empty.");
        }

        _shoppingCartService.AddProduct(userId, shoppingCartProduct);

        return StatusCode(201);
    }

    [HttpPatch("update-quantity")]
    public IActionResult UpdateQuantityInShoppingCart([FromBody] UpdateItemQuantityDto updateItemQuantity)
    {
        _shoppingCartService.UpdateQuantity(updateItemQuantity);

        return StatusCode(200);
    }

    [HttpDelete("remove-item/{itemid}")]
    public IActionResult RemoveProductFromShoppingCart(int itemid)
    {
        _shoppingCartService.RemoveProduct(itemid);

        return StatusCode(200);
    }

    [HttpDelete("clear-cart")]
    public IActionResult ClearShoppingCart()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId.IsNullOrEmpty())
        {
            return StatusCode(401);
        }

        _shoppingCartService.ClearCart(userId);

        return StatusCode(200);
    }
}