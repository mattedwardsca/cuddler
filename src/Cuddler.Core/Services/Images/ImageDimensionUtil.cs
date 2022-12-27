namespace Cuddler.Core.Services.Images;

public static class ImageDimensionUtil
{
    public static ImageDimension GetThumbnailSize(EThumbnailSize size)
    {
        return size switch
        {
            EThumbnailSize.Large => new ImageDimension(1024, 1024),
            EThumbnailSize.Medium => new ImageDimension(600, 600),
            EThumbnailSize.Small => new ImageDimension(110, 110),
            EThumbnailSize.Mini => new ImageDimension(30, 30),
            EThumbnailSize.Inherit => new ImageDimension(0, 0),
            var _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
        };
    }
}