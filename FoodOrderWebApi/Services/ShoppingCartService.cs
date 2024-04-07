using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;

namespace FoodOrderWebApi.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly ShoppingCartRepository _shoppingCartRepository;
    private readonly IMapper _mapper;

    public ShoppingCartService(ShoppingCartRepository shoppingCartRepository, IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
    }

    public void AddProduct(string userId, int productId, int quantity)
    {
        _shoppingCartRepository.AddProduct(userId, productId, quantity);
    }

    public void RemoveOneProduct(string userId, int productId)
    {
        _shoppingCartRepository.RemoveOneProduct(userId, productId);
    }

    public void RemoveProduct(string userId, int productId)
    {
        _shoppingCartRepository.RemoveProduct(userId, productId);
    }

    public List<ShoppingCartItemDto> GetCartByUserId(string userId)
    {
        var shoppingCartItems = _shoppingCartRepository.GetAllItemByUserId(userId);
        return _mapper.Map<List<ShoppingCartItemDto>>(shoppingCartItems);
    }

    public void ClearCart(string userId)
    {
        _shoppingCartRepository.ClearCart(userId);
    }
}