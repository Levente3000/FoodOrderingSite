using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderWebApi.Models;

public class ShoppingCartItem
{
    public int ShoppingCartItemId { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }
}