using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderWebApi.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    public string OrdererName { get; set; } = null!;

    [Required]
    public string OrdererAddress { get; set; } = null!;

    [Required]
    public string OrdererPhoneNumber { get; set; } = null!;

    [Required]
    public bool IsDone { get; set; }
    
    [Required]
    public int RestaurantId { get; set; }
    [ForeignKey("RestaurantId")]
    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;
}