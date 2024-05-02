using FoodOrderWebApi.DTOs.CreateProduct;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IProductService
{
    public CreateEditProductDto GetProductByIdForEdit(int id);

    public Task<int> CreateProduct(CreateEditProductDto createEditProduct);

    public Task<int?> EditProduct(CreateEditProductDto createEditProduct);
}