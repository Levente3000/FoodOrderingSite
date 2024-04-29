using FoodOrderWebApi.DTOs.CreateProduct;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IProductService
{
    public CreateEditProductDto GetProductByIdForEdit(int id);

    public void CreateProduct(CreateEditProductDto createEditRestaurant);

    public void EditRestaurant(CreateEditProductDto createEditRestaurant);
}