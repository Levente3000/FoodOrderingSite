using Microsoft.EntityFrameworkCore;
using System;

namespace FoodOrderWebApi.Configuration
{
    public class FoodOrderDbContext : DbContext
    {
        public FoodOrderDbContext(DbContextOptions<FoodOrderDbContext> options) : base(options)
        {
        }
    }
}
