using Microsoft.AspNetCore.StaticFiles;

namespace FoodOrderWebApi.Services;

public class AssetsService
{
    private readonly IWebHostEnvironment _env;

    public AssetsService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public (Stream fileStream, string contentType) GetAssetForRestaurant(string assetName)
    {
        var filePath = Path.Combine(_env.ContentRootPath, "assets/restaurant", assetName);
        var contentType = GetMimeType(filePath);
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return (fileStream, contentType);
    }

    public (Stream fileStream, string contentType) GetAssetForCategory(string assetName)
    {
        var filePath = Path.Combine(_env.ContentRootPath, "assets/category", assetName);
        var contentType = GetMimeType(filePath);
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return (fileStream, contentType);
    }

    public (Stream fileStream, string contentType) GetAssetForProduct(string assetName)
    {
        var filePath = Path.Combine(_env.ContentRootPath, "assets/product", assetName);
        var contentType = GetMimeType(filePath);
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return (fileStream, contentType);
    }

    private static string GetMimeType(string filePath)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var mimeType))
        {
            mimeType = "application/octet-stream"; // Default MIME type if not found
        }

        return mimeType;
    }
}