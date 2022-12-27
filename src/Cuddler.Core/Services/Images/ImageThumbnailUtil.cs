using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Core.Services.Images;

public static class ImageThumbnailUtil
{
    //public static void CreateThumbnailImage(IFormFile x, int height, int width, string path)
    //{
    //    //using (var image = MediaTypeNames.Image.FromStream(x.OpenReadStream(), true, true))
    //    using (var inStream = x.OpenReadStream())
    //    {
    //        //using (var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero))
    //        //{
    //        //    var jpgInfo = ImageCodecInfo.GetImageEncoders()
    //        //                                .FirstOrDefault(codecInfo => codecInfo.MimeType == "image/jpeg");
    //        //    if (jpgInfo == null)
    //        //    {
    //        //        throw new Exception("mime type not supported");
    //        //    }

    //        //    using (var encParams = new EncoderParameters(1))
    //        //    {
    //        //        const long quality = 100;
    //        //        encParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
    //        //        thumb.Save(path, jpgInfo, encParams);
    //        //    }
    //        //}
    //        throw new NotImplementedException();
    //    }
    //}

    public static bool CanGenerateThumb(string extension)
    {
        bool ret;
        if (string.IsNullOrEmpty(extension))
        {
            ret = false;
        }
        else
        {
            ret = extension switch
            {
                "jpg" => true,
                "jpeg" => true,
                "jif" => true,
                "jfif" => true,
                "jp2" => true,
                "jpx" => true,
                "j2k" => true,
                "j2c" => true,
                "tif" => true,
                "tiff" => true,
                "png" => true,
                "gif" => true,
                "bmp" => true,
                var _ => false
            };
        }

        return ret;
    }

    public static Image GetReducedImage(Stream stream, int width, int height)
    {
        var image = Image.FromStream(stream);
        var size = image.Size;
        var newSize = ResizeKeepAspect(size, width, height, true);
        var thumb = image.GetThumbnailImage(newSize.Width, newSize.Height, () => false, IntPtr.Zero);

        return thumb;
    }

    public static MemoryStream GetThumbnailImage(IFormFile x)
    {
        var stream = x.OpenReadStream();
        var newImage = GetReducedImage(stream, 200, 200);
        //var bytes = (byte[]) TypeDescriptor.GetConverter(newImage).ConvertTo(newImage, typeof(byte[]));
        //var result = Convert.ToBase64String(bytes);
        var ms = new MemoryStream();
        newImage.Save(ms, ImageFormat.Png);
        ms.Position = 0; // If you're going to read from the stream, you may need to reset the position to the start

        return ms;
    }

    public static Size ResizeKeepAspect(Size src, int maxWidth, int maxHeight, bool enlarge = false)
    {
        maxWidth = enlarge
            ? maxWidth
            : Math.Min(maxWidth, src.Width);
        maxHeight = enlarge
            ? maxHeight
            : Math.Min(maxHeight, src.Height);
        var rnd = Math.Min(maxWidth / (decimal)src.Width, maxHeight / (decimal)src.Height);

        return new Size((int)Math.Round(src.Width * rnd), (int)Math.Round(src.Height * rnd));
    }
}