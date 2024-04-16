using System.ComponentModel.DataAnnotations;

namespace FoodOrderWebApi.Models;

public class PromoCode
{
    public int Id { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    [Range(0.0, 1.0)]
    public double Percentage { get; set; }
}