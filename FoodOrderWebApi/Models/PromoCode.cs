using System.ComponentModel.DataAnnotations;

namespace FoodOrderWebApi.Models;

public class PromoCode
{
    public int Id { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    public int Percentage { get; set; }
}