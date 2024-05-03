using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.ShoppingCart;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Services.Interfaces;

namespace FoodOrderWebApi.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMapper _mapper;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
    }

    public void AddProduct(string userId, ShoppingCartProductDto shoppingCartProductDto)
    {
        var shoppingCartItem =
            _shoppingCartRepository.GetItemByUserIdAndProductId(userId, shoppingCartProductDto.ProductId);
        if (shoppingCartItem == null)
        {
            _shoppingCartRepository.AddProduct(userId, shoppingCartProductDto.ProductId,
                shoppingCartProductDto.Quantity);
        }
        else
        {
            shoppingCartItem.Quantity += shoppingCartProductDto.Quantity;
            _shoppingCartRepository.UpdateProduct(shoppingCartItem);
        }
    }

    public void UpdateQuantity(UpdateItemQuantityDto updateItemQuantity)
    {
        var cartItem = _shoppingCartRepository.GetItemByCartItemId(updateItemQuantity.ShoppingCartItemId);

        if (cartItem == null)
        {
            throw new Exception();
        }

        cartItem.Quantity = updateItemQuantity.Quantity;

        _shoppingCartRepository.UpdateProduct(cartItem);
    }

    public void RemoveProduct(int shoppingCartItemId)
    {
        var product = _shoppingCartRepository.GetItemByCartItemId(shoppingCartItemId);

        if (product == null)
        {
            throw new Exception();
        }

        _shoppingCartRepository.RemoveProduct(product);
    }

    public List<ShoppingCartItemDto> GetCartByUserId(string userId)
    {
        var shoppingCartItems =
            _shoppingCartRepository.GetAllItemByUserId(userId);

        var shoppingCartItemsDtoList = shoppingCartItems
            .Select(item => new ShoppingCartItemDto
            {
                ProductId = item.ProductId,
                Product = _mapper.Map<ProductDto>(item.Product),
                Quantity = item.Quantity,
                ShoppingCartItemId = item.ShoppingCartItemId,
                UserId = item.UserId,
                RestaurantName = item.Product.Restaurant.Name
            }).ToList();

        return shoppingCartItemsDtoList;
    }

    public void ClearCart(string userId)
    {
        _shoppingCartRepository.ClearCart(userId);
    }
}