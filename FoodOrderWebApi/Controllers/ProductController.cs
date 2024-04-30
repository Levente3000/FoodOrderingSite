using System.Security.Claims;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.CreateProduct;
using FoodOrderWebApi.DTOs.CreateRestaurant;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Controllers;

[Route("product")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{productId}")]
    public CreateEditProductDto GetProductForEdit(int productId)
    {
        return _productService.GetProductByIdForEdit(productId);
    }

    [HttpPost("create-product")]
    public Task<int> CreateProduct([FromForm] CreateEditProductDto createEditProduct)
    {
        return _productService.CreateProduct(createEditProduct);
    }

    [HttpPost("edit-product")]
    public Task<int?> EditProduct([FromForm] CreateEditProductDto createEditProduct)
    {
        return _productService.EditProduct(createEditProduct);
    }
}