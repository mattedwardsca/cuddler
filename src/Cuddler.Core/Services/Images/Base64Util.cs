using System.Net;

namespace Cuddler.Core.Services.Images;

public static class Base64Util
{
    public static byte[]? ConvertBase64ToBytes(string? base64String)
    {
        if (string.IsNullOrEmpty(base64String))
        {
            return null;
        }

        base64String = RemoveDataContentTypeBase64(base64String);

        return Convert.FromBase64String(base64String);
    }

    public static async Task<string> GetImageAsBase64Url(string url, EImageType imageType)
    {
        try
        {
            var credentials = new NetworkCredential();
            using var handler = new HttpClientHandler
            {
                Credentials = credentials
            };

            using var client = new HttpClient(handler);
            var bytes = await client.GetByteArrayAsync(url);

            if (imageType == EImageType.Svg)
            {
                return "data:image/svg+xml;base64," + Convert.ToBase64String(bytes);
            }

            return $"data:image/{imageType.ToString().ToLower()};base64," + Convert.ToBase64String(bytes);
        }
        catch (HttpRequestException)
        {
            return url;
        }
    }

    public static string RemoveDataContentTypeBase64(string base64String)
    {
        if (base64String.StartsWith("data:image/png;base64,"))
        {
            base64String = base64String[22..]; // removes data:image/png;base64,
        }

        return base64String;
    }
}