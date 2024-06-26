﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderWebApi.Models;

public class OrderItem
{
    public int Id { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }
}