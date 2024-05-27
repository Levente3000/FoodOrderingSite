using FoodOrderWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Controllers;

[Route("assets")]
[ApiController]
public class AssetsController : Controller
{
    private readonly IAssetsService _assetsService;

    public AssetsController(IAssetsService assetsService)
    {
        _assetsService = assetsService;
    }

    [HttpGet("restaurant/{assetName}")]
    public FileStreamResult GetAssetForRestaurant(string assetName)
    {
        var (fileStream, contentType) = _assetsService.GetAsset("restaurant", assetName);

        return File(fileStream, contentType);
    }

    [HttpGet("category/{assetName}")]
    public FileStreamResult GetAssetForCategory(string assetName)
    {
        var (fileStream, contentType) = _assetsService.GetAsset("category", assetName);

        return File(fileStream, contentType);
    }

    [HttpGet("product/{assetName}")]
    public FileStreamResult GetAssetForProduct(string assetName)
    {
        var (fileStream, contentType) = _assetsService.GetAsset("product", assetName);

        return File(fileStream, contentType);
    }
}