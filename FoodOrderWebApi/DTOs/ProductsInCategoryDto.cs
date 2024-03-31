using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.DTOs;

public class ProductsInCategoryDto
{
    public string Name { get; set; } = null!;

    public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
}