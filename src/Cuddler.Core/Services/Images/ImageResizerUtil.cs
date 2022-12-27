using SkiaSharp;

namespace Cuddler.Core.Services.Images;

public static class ImageResizerUtil
{
    public static (byte[] FileContents, int Height, int Width) Resize(byte[] fileContents, int maxWidth, int maxHeight, bool square, SKFilterQuality quality = SKFilterQuality.High)
    {
        var paddedHeight = maxHeight;
        var paddedWidth = maxWidth;

        var bitmap = SKBitmap.Decode(fileContents);
        var bitmapRatio = (float)bitmap.Width / bitmap.Height;
        var resizeRatio = (float)maxWidth / maxHeight;

        if (bitmapRatio > resizeRatio) // original is more "landscape"
        {
            maxHeight = (int)Math.Round(bitmap.Height * ((float)maxWidth / bitmap.Width));
        }
        else
        {
            maxWidth = (int)Math.Round(bitmap.Width * ((float)maxHeight / bitmap.Height));
        }

        var resizedImageInfo = new SKImageInfo(maxWidth, maxHeight, SKImageInfo.PlatformColorType, bitmap.AlphaType);
        var resizedBitmap = bitmap.Resize(resizedImageInfo, quality);

        resizedBitmap = square
            ? RecreateWithPadding(resizedBitmap, paddedWidth, paddedHeight, true) // add padding to make it square
            : RecreateWithPadding(resizedBitmap, resizedBitmap.Width, resizedBitmap.Height, true); // no padding

        var resizedImage = SKImage.FromBitmap(resizedBitmap);
        const SKEncodedImageFormat encodeFormat = SKEncodedImageFormat.Jpeg;
        var imageData = resizedImage.Encode(encodeFormat, 90);

        return (imageData.ToArray(), maxHeight, maxWidth);
    }

    private static SKBitmap RecreateWithPadding(SKBitmap original, int paddedWidth, int paddedHeight, bool isOpaque)
    {
        // setup new bitmap and optionally clear
        var bitmap = new SKBitmap(paddedWidth, paddedHeight, isOpaque);
        var canvas = new SKCanvas(bitmap);
        canvas.Clear(isOpaque
            ? SKColors.White
            : SKColor.Empty); // we could make this color a resizeParam

        // find co-ords to draw original at
        var left = original.Width < paddedWidth
            ? (paddedWidth - original.Width) / 2
            : 0;
        var top = original.Height < paddedHeight
            ? (paddedHeight - original.Height) / 2
            : 0;

        var drawRect = new SKRectI
        {
            Left = left,
            Top = top,
            Right = original.Width + left,
            Bottom = original.Height + top
        };

        // draw original onto padded version
        canvas.DrawBitmap(original, drawRect);
        canvas.Flush();

        canvas.Dispose();
        original.Dispose();

        return bitmap;
    }
}