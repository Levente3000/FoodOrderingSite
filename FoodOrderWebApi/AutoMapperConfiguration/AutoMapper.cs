using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.AutoMapperConfiguration;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));
        CreateMap<FoodCategory, FoodCategoryDto>();
    }
}