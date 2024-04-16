using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Enum;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Controllers;

[Route("promo-code")]
[ApiController]
public class PromoCodeController : Controller
{
    private readonly IRepository<PromoCode, int> _promoCodeRepository;

    public PromoCodeController(IRepository<PromoCode, int> promoCodeRepository)
    {
        _promoCodeRepository = promoCodeRepository;
    }

    [HttpGet]
    public List<PromoCode> GetAllPriceCategory()
    {
        return _promoCodeRepository.GetAll();
    }
}