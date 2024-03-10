using Microsoft.AspNetCore.StaticFiles;

namespace FoodOrderWebApi.Services;

public class AssetsService
{
    private readonly IWebHostEnvironment _env;
    private const string Assetpath = "assets";

    public AssetsService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public (Stream fileStream, string contentType) GetAssetForRestaurant(string assetName)
    {
        var filePath = Path.Combine(_env.ContentRootPath, Assetpath + "/restaurant", assetName);
        var contentType = GetMimeType(filePath);
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return (fileStream, contentType);
    }

    public (Stream fileStream, string contentType) GetAssetForCategory(string assetName)
    {
        var filePath = Path.Combine(_env.ContentRootPath, Assetpath + "/category", assetName);
        var contentType = GetMimeType(filePath);
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return (fileStream, contentType);
    }

    public (Stream fileStream, string contentType) GetAssetForProduct(string assetName)
    {
        var filePath = Path.Combine(_env.ContentRootPath, Assetpath + "/product", assetName);
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