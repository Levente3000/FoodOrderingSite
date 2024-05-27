using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

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
    public string UserId { get; set; } = null!;
    
    public Instant CreatedAt { get; set; }

    [ForeignKey("PromoCode")]
    public int? PromoCodeId { get; set; }

    public virtual PromoCode PromoCode { get; set; } = null!;

    [Required]
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = null!;
}