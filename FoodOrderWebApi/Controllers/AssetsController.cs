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

    [HttpGet("restaurant")]
    public FileStreamResult GetAssetForRestaurant(string assetName)
    {
        var (fileStream, contentType) = _assetsService.GetAssetForRestaurant(assetName);

        return File(fileStream, contentType);
    }

    [HttpGet("category")]
    public FileStreamResult GetAssetForCategory(string assetName)
    {
        var (fileStream, contentType) = _assetsService.GetAssetForCategory(assetName);

        return File(fileStream, contentType);
    }

    [HttpGet("product")]
    public FileStreamResult GetAssetForProduct(string assetName)
    {
        var (fileStream, contentType) = _assetsService.GetAssetForProduct(assetName);

        return File(fileStream, contentType);
    }
}