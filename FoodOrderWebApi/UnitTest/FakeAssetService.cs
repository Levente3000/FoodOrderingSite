using FoodOrderWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace UnitTest;

public class FakeAssetService : IAssetsService
{
    public (Stream fileStream, string contentType) GetAsset(string assetPath, string assetName)
    {
        throw new NotImplementedException();
    }

    public Task SaveAssetIfNotExists(IFormFile file, string directory)
    {
        throw new NotImplementedException();
    }
}