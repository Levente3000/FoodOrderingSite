using AutoMapper;
using FoodOrderWebApi.DTOs.CreateProduct;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IFoodCategoryRepository _foodCategoryRepository;
    private readonly AssetsService _assetService;
    private readonly IMapper _mapper;
    private readonly string DIRECTORY = "product";

    public ProductService(IProductRepository productsRepository, IFoodCategoryRepository foodCategoryRepository,
        IMapper mapper, AssetsService assetService)
    {
        _productRepository = productsRepository;
        _foodCategoryRepository = foodCategoryRepository;
        _assetService = assetService;
        _mapper = mapper;
    }

    public CreateEditProductDto GetProductByIdForEdit(int id)
    {
        return _mapper.Map<CreateEditProductDto>(_productRepository.GetProductById(id));
    }

    public async Task<int> CreateProduct(CreateEditProductDto createEditProduct)
    {
        if (createEditProduct.Picture != null)
        {
            await _assetService.SaveAssetIfNotExists(createEditProduct.Picture, DIRECTORY);
        }

        var product = _mapper.Map<Product>(createEditProduct);

        product.Categories = _foodCategoryRepository
            .GetCategoriesByNameList(createEditProduct.CategoryNames.ToList());

        _productRepository.CreateProduct(product);

        return createEditProduct.RestaurantId;
    }

    public async Task<int?> EditProduct(CreateEditProductDto createEditProduct)
    {
        Product? product = null;
        if (createEditProduct.Id.HasValue)
        {
            product = _productRepository.GetProductByIdAsTracking(createEditProduct.Id.Value);
        }

        if (product == null)
        {
            return null;
        }

        if (createEditProduct.Picture?.FileName != null)
        {
            await _assetService.SaveAssetIfNotExists(createEditProduct.Picture, DIRECTORY);
        }

        _mapper.Map(createEditProduct, product);

        product.Categories.Clear();

        var categoriesToAdd = _foodCategoryRepository
            .GetCategoriesByNameList(createEditProduct.CategoryNames.ToList());

        foreach (var category in categoriesToAdd)
        {
            product.Categories.Add(category);
        }

        _productRepository.UpdateProduct(product);

        return createEditProduct.RestaurantId;
    }
}