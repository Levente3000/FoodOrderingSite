using System.Diagnostics;
using FoodOrderWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Controllers;

[Route("assets")]
[ApiController]
public class AssetsController : Controller
{
    private readonly AssetsService _assetsService;

    public AssetsController(AssetsService assetsService)
    {
        _assetsService = assetsService;
    }

    [HttpGet]
    public FileStreamResult GetLogo(string companyLogoName)
    {
        var (fileStream, contentType) = _assetsService.GetLogos(companyLogoName);

        return File(fileStream, contentType);
    }
}