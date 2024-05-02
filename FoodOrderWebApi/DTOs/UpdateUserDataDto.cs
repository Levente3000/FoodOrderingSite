namespace FoodOrderWebApi.DTOs;

public class UpdateUserDataDto
{
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;
}