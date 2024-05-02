using System.Globalization;
using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.DTOs.CreateProduct;
using FoodOrderWebApi.DTOs.CreateRestaurant;
using FoodOrderWebApi.DTOs.Order;
using FoodOrderWebApi.Models;
using Microsoft.OpenApi.Models;
using NodaTime;
using NodaTime.Text;

namespace FoodOrderWebApi.AutoMapperConfiguration;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<Restaurant, RestaurantDetailsDto>();

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));
        CreateMap<ProductDto, Product>();

        CreateMap<FoodCategory, FoodCategoryDto>();
        CreateMap<FoodCategory, ProductsInCategoryDto>();

        CreateMap<OpeningHour, OpeningHourDto>()
            .ForMember(dto => dto.Monday, conf => conf.MapFrom(oh => FormatInstant(oh.Monday)))
            .ForMember(dto => dto.Tuesday, conf => conf.MapFrom(oh => FormatInstant(oh.Tuesday)))
            .ForMember(dto => dto.Wednesday, conf => conf.MapFrom(oh => FormatInstant(oh.Wednesday)))
            .ForMember(dto => dto.Thursday, conf => conf.MapFrom(oh => FormatInstant(oh.Thursday)))
            .ForMember(dto => dto.Friday, conf => conf.MapFrom(oh => FormatInstant(oh.Friday)))
            .ForMember(dto => dto.Saturday, conf => conf.MapFrom(oh => FormatInstant(oh.Saturday)))
            .ForMember(dto => dto.Sunday, conf => conf.MapFrom(oh => FormatInstant(oh.Sunday)));

        CreateMap<CreateRestaurantOpeningHoursDto, OpeningHour>()
            .ForMember(dto => dto.Monday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Monday)))
            .ForMember(dto => dto.Tuesday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Tuesday)))
            .ForMember(dto => dto.Wednesday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Wednesday)))
            .ForMember(dto => dto.Thursday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Thursday)))
            .ForMember(dto => dto.Friday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Friday)))
            .ForMember(dto => dto.Saturday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Saturday)))
            .ForMember(dto => dto.Sunday, conf => conf.MapFrom(oh => ConvertTimeToInstant(oh.Sunday)));

        CreateMap<ShoppingCartItem, ShoppingCartItemDto>();
        CreateMap<ShoppingCartItemDto, ShoppingCartItem>();

        CreateMap<CreateEditRestaurantDto, Restaurant>()
            .ForMember(dest => dest.LogoName, opt => opt.MapFrom((src, dest) =>
                src.Logo != null ? src.Logo.FileName : dest.LogoName))
            .ForMember(dest => dest.BannerName, opt => opt.MapFrom((src, dest) =>
                src.Banner != null ? src.Banner.FileName : dest.BannerName))
            .ForMember(dest => dest.OpeningHours, opt => opt.MapFrom(src => MapOpeningHours(src.OpeningHours)))
            .ForMember(dest => dest.ClosingHours, opt => opt.MapFrom(src => MapOpeningHours(src.ClosingHours)));

        CreateMap<Restaurant, CreateEditRestaurantDto>()
            .ForMember(dest => dest.OpeningHours,
                opt => opt.MapFrom(src => MapOpeningHoursFromInstantToString(src.OpeningHours)))
            .ForMember(dest => dest.ClosingHours,
                opt => opt.MapFrom(src => MapOpeningHoursFromInstantToString(src.ClosingHours)));

        CreateMap<CreateEditProductDto, Product>()
            .ForMember(dest => dest.PictureName, opt => opt.MapFrom((src, dest) =>
                src.Picture != null ? src.Picture.FileName : dest.PictureName));

        CreateMap<Product, CreateEditProductDto>()
            .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));

        CreateMap<UpdateUserDataDto, UserData>();

        CreateMap<ShoppingCartItemDto, OrderItem>()
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
    }

    private static string? FormatInstant(Instant? instant) =>
        instant?.ToDateTimeOffset().ToString("o");

    private static OpeningHour MapOpeningHours(CreateRestaurantOpeningHoursDto dto)
    {
        return new OpeningHour
        {
            Monday = ConvertTimeToInstant(dto.Monday),
            Tuesday = ConvertTimeToInstant(dto.Tuesday),
            Wednesday = ConvertTimeToInstant(dto.Wednesday),
            Thursday = ConvertTimeToInstant(dto.Thursday),
            Friday = ConvertTimeToInstant(dto.Friday),
            Saturday = ConvertTimeToInstant(dto.Saturday),
            Sunday = ConvertTimeToInstant(dto.Sunday)
        };
    }

    private static Instant? ConvertTimeToInstant(string? time)
    {
        if (time is "null" or null)
        {
            return null;
        }

        var pattern = LocalTimePattern.CreateWithInvariantCulture("h:mm tt");
        var parseResult = pattern.Parse(time);

        if (!parseResult.Success)
        {
            throw new ArgumentException($"Invalid time format: {time}");
        }

        var referenceDate = new LocalDate(2000, 1, 1);
        var dateTime = referenceDate.At(parseResult.Value);

        return dateTime.InUtc().ToInstant();
    }

    private static CreateRestaurantOpeningHoursDto MapOpeningHoursFromInstantToString(OpeningHour openingHour)
    {
        return new CreateRestaurantOpeningHoursDto
        {
            Monday = FormatTimeFromInstantToString(openingHour.Monday),
            Tuesday = FormatTimeFromInstantToString(openingHour.Tuesday),
            Wednesday = FormatTimeFromInstantToString(openingHour.Wednesday),
            Thursday = FormatTimeFromInstantToString(openingHour.Thursday),
            Friday = FormatTimeFromInstantToString(openingHour.Friday),
            Saturday = FormatTimeFromInstantToString(openingHour.Saturday),
            Sunday = FormatTimeFromInstantToString(openingHour.Sunday)
        };
    }

    private static string FormatTimeFromInstantToString(Instant? instant)
    {
        if (!instant.HasValue)
        {
            return string.Empty;
        }

        LocalDateTime localDateTime = instant.Value.InUtc().LocalDateTime;
        var pattern = LocalTimePattern.CreateWithInvariantCulture("hh:mm tt");
        return pattern.Format(localDateTime.TimeOfDay);
    }
}