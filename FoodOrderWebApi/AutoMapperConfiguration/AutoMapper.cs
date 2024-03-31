using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
using Microsoft.OpenApi.Models;
using NodaTime;

namespace FoodOrderWebApi.AutoMapperConfiguration;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<Restaurant, RestaurantDetailsDto>();

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));

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
    }

    private static string? FormatInstant(Instant? instant) =>
        instant?.ToDateTimeOffset().ToString("o");
}