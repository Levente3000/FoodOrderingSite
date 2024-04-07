using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Configuration;

public class FoodOrderDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<OpeningHour> OpeningHours { get; set; }

    public DbSet<FoodCategory> FoodCategories { get; set; }


    public DbSet<Order> Orders { get; set; }

    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<ShoppingCart> ShoppingCart { get; set; }

    public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

    public FoodOrderDbContext(DbContextOptions<FoodOrderDbContext> options) : base(options)
    {
    }
}