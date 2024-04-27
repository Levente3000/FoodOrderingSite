using System.ComponentModel.DataAnnotations;

namespace FoodOrderWebApi.Models;

public class RestaurantPermission
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public int RestaurantId { get; set; }
}