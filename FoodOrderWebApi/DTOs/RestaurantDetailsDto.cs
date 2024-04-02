using FoodOrderWebApi.Enum;

namespace FoodOrderWebApi.DTOs;

public class RestaurantDetailsDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string LogoName { get; set; } = null!;

    public string BannerName { get; set; } = null!;

    public PriceCategory PriceCategory { get; set; }

    public ICollection<ProductsInCategoryDto>? CategoriesWithProducts { get; set; } = new List<ProductsInCategoryDto>();

    public virtual OpeningHourDto OpeningHours { get; set; } = null!;

    public virtual OpeningHourDto ClosingHours { get; set; } = null!;
}