using FoodOrderWebApi.Enum;
using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace FoodOrderWebApi.Configuration.EntityConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(r => r.CreatedAt)
            .HasConversion(v => v.ToDateTimeUtc(),
                v => Instant.FromDateTimeUtc(DateTime.SpecifyKind(v, DateTimeKind.Utc)))
            .HasDefaultValueSql("now()");
    }
}