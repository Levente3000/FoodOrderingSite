namespace FoodOrderWebApi.DTOs.CreateProduct;

public class CreateEditProductDto
{
    public int? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Price { get; set; }
    public IFormFile? Picture { get; set; } = null!;
    public bool IsEnabled { get; set; }
    public int RestaurantId { get; set; }
    public ICollection<string> CategoryNames { get; set; } = new List<string>();
}