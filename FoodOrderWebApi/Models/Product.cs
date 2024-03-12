﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderWebApi.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public int Price { get; set; }

    [Required]
    public string PictureName { get; set; } = null!;

    [Required]
    public bool IsEnabled { get; set; }

    [Required]
    [ForeignKey("Category")]
    public string CategoryName { get; set; } = null!;

    public virtual ICollection<FoodCategory> Category { get; set; } = null!;

    [Required]
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = null!;
}