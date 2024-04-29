using AutoMapper;
using FoodOrderWebApi.DTOs.CreateProduct;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services.Interfaces;

namespace FoodOrderWebApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly AssetsService _assetService;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productsRepository, IMapper mapper, AssetsService assetService)
    {
        _productRepository = productsRepository;
        _assetService = assetService;
        _mapper = mapper;
    }

    public CreateEditProductDto GetProductByIdForEdit(int id)
    {
        return _mapper.Map<CreateEditProductDto>(_productRepository.GetProductById(id));
    }

    public void CreateProduct(CreateEditProductDto createEditRestaurant)
    {
    }

    public void EditRestaurant(CreateEditProductDto createEditRestaurant)
    {
    }
}