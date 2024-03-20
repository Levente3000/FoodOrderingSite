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
}