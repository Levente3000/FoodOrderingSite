using FoodOrderWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoodOrderWebApi.Configuration
{
    public class FoodOrderDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<OpeningHour> OpeningHours { get; set; }
        public DbSet<FoodCategory> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public FoodOrderDbContext(DbContextOptions<FoodOrderDbContext> options) : base(options)
        {
        }
    }
}
