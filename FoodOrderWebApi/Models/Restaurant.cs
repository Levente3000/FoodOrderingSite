using FoodOrderWebApi.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace FoodOrderWebApi.Models;

public class Restaurant
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public string LogoName { get; set; } = null!;

    [Required]
    public string BannerName { get; set; } = null!;

    public PriceCategory PriceCategory
    {
        get
        {
            if (Products == null || Products.Count == 0)
                return PriceCategory.NoProduct;

            var averagePrice = Products.Average(p => p.Price);

            return averagePrice switch
            {
                <= 1500 => PriceCategory.Low,
                <= 4000 => PriceCategory.Medium,
                _ => PriceCategory.High
            };
        }
        set { }
    }

    public Instant CreatedAt { get; set; }

    [Required]
    public int OpeningHourId { get; set; }

    [ForeignKey("OpeningHourId")]
    public virtual OpeningHour OpeningHours { get; set; } = null!;

    [Required]
    public int ClosingHourId { get; set; }

    [ForeignKey("ClosingHourId")]
    public virtual OpeningHour ClosingHours { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = null!;
}