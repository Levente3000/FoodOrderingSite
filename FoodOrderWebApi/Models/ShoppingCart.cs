using System.ComponentModel.DataAnnotations;

namespace FoodOrderWebApi.Models;

public class ShoppingCart
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    public virtual ICollection<ShoppingCartItem> Items { get; set; } = null!;
}