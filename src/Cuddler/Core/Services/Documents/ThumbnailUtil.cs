using BoostDC.Libs.Images;

namespace Cuddler.Core.Services.Documents;

public static class ThumbnailUtil
{
    public static string GetPackageThumbnail(byte[]? thumbnailImage)
    {
        if (thumbnailImage == null)
        {
            return IconLinks.Package;
        }

        var imageString = Convert.ToBase64String(thumbnailImage);

        return $"data:image/png;base64,{imageString}";
    }

    public static string GetPackageThumbnail(IHasThumbnailId model, int size = 2)
    {
        return !string.IsNullOrEmpty(model.ThumbnailId)
            ? GetDownloadLink(model.ThumbnailId, size)
            : IconLinks.Package;

    }

    private static string GetDownloadLink(string thumbnailId, int size)
    {

        if (size == 1)
        {
            return $"/Apis/Core/Documents/Image/{thumbnailId}?w=50&h=50";
        }

        return $"/Apis/Core/Documents/Image/{thumbnailId}?w=400&h=400";
    }


    public static string GetThumbnail(string? thumbnailId, int size = 2)
    {
        return string.IsNullOrEmpty(thumbnailId)
            ? string.Empty
            : GetDownloadLink(thumbnailId, size);
    }
}