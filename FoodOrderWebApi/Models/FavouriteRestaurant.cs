using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderWebApi.Models;

public class FavouriteRestaurant
{
    public int Id { get; set; }

    [Required]
    public string userId { get; set; } = null!;

    [Required]
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;
}