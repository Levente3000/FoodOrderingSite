using Microsoft.AspNetCore.StaticFiles;

namespace FoodOrderWebApi.Services;

public class AssetsService
{
    private readonly IWebHostEnvironment _env;
    private const string AssetPath = "assets";

    public AssetsService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public (Stream fileStream, string contentType) GetAsset(string assetPath, string assetName)
    {
        var filePath = Path.Combine(_env.ContentRootPath, $"{AssetPath}/{assetPath}", assetName);
        var contentType = GetMimeType(filePath);
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return (fileStream, contentType);
    }

    public async Task SaveAssetIfNotExists(IFormFile file, string directory)
    {
        var filePath = Path.Combine(_env.ContentRootPath, $"{AssetPath}/{directory}", file.FileName);

        if (!File.Exists(filePath))
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
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