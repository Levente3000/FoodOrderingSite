using FoodOrderWebApi.Helper;

namespace FoodOrderWebApi.Services;

public class AssetsService
{
    private readonly IWebHostEnvironment _env;

    public AssetsService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public byte[] GetLogo(string companyLogoName)
    {
        return File.ReadAllBytes($@"{_env.ContentRootPath}\assets\{companyLogoName}");
    }

    public (Stream fileStream, string contentType) GetAssets(string assetName)
    {
        var filePath = Path.Combine(_env.ContentRootPath, "assets", assetName);
        var contentType = MimeHelper.GetMimeType(filePath);
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return (fileStream, contentType);
    }
}