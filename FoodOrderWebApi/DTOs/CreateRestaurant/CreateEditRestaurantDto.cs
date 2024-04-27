namespace FoodOrderWebApi.DTOs.CreateRestaurant;

public class CreateEditRestaurantDto
{
    public int? Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public IFormFile? Logo { get; set; }

    public IFormFile? Banner { get; set; }

    public CreateRestaurantOpeningHoursDto OpeningHours { get; set; } = null!;

    public CreateRestaurantOpeningHoursDto ClosingHours { get; set; } = null!;
}