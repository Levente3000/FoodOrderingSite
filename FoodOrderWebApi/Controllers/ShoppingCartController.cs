using System.Security.Claims;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrderWebApi.Controllers;

[Route("shopping-cart")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet]
    public List<ShoppingCartItemDto> GetShoppingCart()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return userId.IsNullOrEmpty() ? null : _shoppingCartService.GetCartByUserId(userId);
    }

    [HttpPost("add-product")]
    public IActionResult AddProductToShoppingCart([FromBody] AddOrRemoveProductDto addOrRemoveProduct)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId.IsNullOrEmpty())
        {
            return StatusCode(401);
        }

        _shoppingCartService.AddProduct(userId, addOrRemoveProduct.ProductId, addOrRemoveProduct.Quantity);

        return StatusCode(201);
    }

    [HttpPatch("remove-one-product")]
    public IActionResult RemoveOneProductFromShoppingCart([FromBody] AddOrRemoveProductDto addOrRemoveProduct)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId.IsNullOrEmpty())
        {
            return StatusCode(401);
        }

        _shoppingCartService.RemoveOneProduct(userId, addOrRemoveProduct.ProductId);

        return StatusCode(200);
    }

    [HttpPatch("update-quantity")]
    public IActionResult UpdateQuantityInShoppingCart([FromBody] UpdateItemQuantityDto updateItemQuantity)
    {
        _shoppingCartService.UpdateQuantity(updateItemQuantity.ShoppingCartItemId, updateItemQuantity.Quantity);

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