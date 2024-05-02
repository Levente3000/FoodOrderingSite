using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Price { get; set; }
    public string PictureName { get; set; } = null!;
    public bool IsEnabled { get; set; }
    public ICollection<string> CategoryNames { get; set; } = new List<string>();
    public int RestaurantId { get; set; }

    public ProductDto()
    {
    }

    public ProductDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        PictureName = product.PictureName;
        IsEnabled = product.IsEnabled;
        CategoryNames = product.Categories.Select(c => c.Name).ToList();
        RestaurantId = product.RestaurantId;
    }
}