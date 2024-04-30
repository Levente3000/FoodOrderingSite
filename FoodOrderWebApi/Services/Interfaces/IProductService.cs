using FoodOrderWebApi.DTOs.CreateProduct;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IProductService
{
    public CreateEditProductDto GetProductByIdForEdit(int id);

    public int CreateProduct(CreateEditProductDto createEditProduct);

    public int? EditProduct(CreateEditProductDto createEditProduct);
}