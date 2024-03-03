using Microsoft.AspNetCore.StaticFiles;

namespace FoodOrderWebApi.Helper;

public class MimeHelper
{
    public static string GetMimeType(string filePath)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var mimeType))
            mimeType = "application/octet-stream"; // Default MIME type if not found

        return mimeType;
    }
}