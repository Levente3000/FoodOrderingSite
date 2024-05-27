namespace FoodOrderWebApi.Services.Interfaces;

public interface IAssetsService
{
    public (Stream fileStream, string contentType) GetAsset(string assetPath, string assetName);

    public Task SaveAssetIfNotExists(IFormFile file, string directory);
}