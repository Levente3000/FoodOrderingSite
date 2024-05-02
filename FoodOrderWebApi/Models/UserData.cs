using System.ComponentModel.DataAnnotations;

namespace FoodOrderWebApi.Models;

public class UserData
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string? Address { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string? Phone { get; set; }
}