﻿using FoodOrderWebApi.Enum;
using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.DTOs;

public class RestaurantDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string LogoName { get; set; } = null!;

    public PriceCategory PriceCategory { get; set; }

    public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();

    public virtual OpeningHourDto OpeningHours { get; set; } = null!;

    public virtual OpeningHourDto ClosingHours { get; set; } = null!;
}