using FoodOrderWebApi.Enum;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Controllers;

[Route("priceCategory")]
[ApiController]
public class PriceCategoryController : Controller
{
    [HttpGet]
    public List<PriceCategory> GetAllPriceCategory()
    {
        return System.Enum.GetValues(typeof(PriceCategory)).Cast<PriceCategory>()
            .Where(priceCategory => priceCategory != PriceCategory.NoProduct)
            .ToList();
    }
}