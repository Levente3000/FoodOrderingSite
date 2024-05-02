using FoodOrderWebApi.Enum;
using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace FoodOrderWebApi.Configuration.EntityConfiguration;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.Property(r => r.CreatedAt)
            .HasConversion(v => v.ToDateTimeUtc(),
                v => Instant.FromDateTimeUtc(DateTime.SpecifyKind(v, DateTimeKind.Utc)))
            .HasDefaultValueSql("now()");

        builder.Property(r => r.PriceCategory)
            .HasDefaultValue(PriceCategory.NoProduct);
    }
}