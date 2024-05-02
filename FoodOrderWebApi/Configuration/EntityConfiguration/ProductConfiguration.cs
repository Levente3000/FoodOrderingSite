using FoodOrderWebApi.Enum;
using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace FoodOrderWebApi.Configuration.EntityConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(r => r.IsEnabled)
            .HasDefaultValue(true);
    }
}