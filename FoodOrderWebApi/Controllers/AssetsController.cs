using System.Diagnostics;
using FoodOrderWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
        var (fileStream, contentType) = _assetsService.GetAsset("restaurant", assetName);

        return File(fileStream, contentType);
    }

    [HttpGet("category")]
    public FileStreamResult GetAssetForCategory(string assetName)
    {
        var (fileStream, contentType) = _assetsService.GetAsset("category", assetName);

        return File(fileStream, contentType);
    }

    [HttpGet("product")]
    public FileStreamResult GetAssetForProduct(string assetName)
    {
        var (fileStream, contentType) = _assetsService.GetAsset("product", assetName);

        return File(fileStream, contentType);
    }
}