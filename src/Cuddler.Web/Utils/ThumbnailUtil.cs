using Cuddler.Data.Entities;

namespace Cuddler.Web.Utils;

public static class ThumbnailUtil
{
    public const string BoostIcon = "/_content/BoostDC.Ui/Icon/BoostIcon.svg";
    public const string FourOhFour = "/_content/BoostDC.Ui/Icon/FourOhFour.svg";
    public const string Package = "/_content/BoostDC.Ui/Icon/Package.svg";
    public const string User = "/_content/BoostDC.Ui/Icon/User.svg";

    public static string GetPackageThumbnail(byte[]? thumbnailImage)
    {
        if (thumbnailImage == null)
        {
            return Package;
        }

        var imageString = Convert.ToBase64String(thumbnailImage);

        return $"data:image/png;base64,{imageString}";
    }

    public static string GetPackageThumbnail(IHasThumbnailId model, int size = 2)
    {
        return !string.IsNullOrEmpty(model.ThumbnailId)
            ? GetDownloadLink(model.ThumbnailId, size)
            : Package;

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