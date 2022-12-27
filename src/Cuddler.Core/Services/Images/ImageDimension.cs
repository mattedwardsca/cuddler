namespace Cuddler.Core.Services.Images;

public class ImageDimension
{
    public ImageDimension()
    {
    }

    public ImageDimension(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Height { get; set; }

    public int Width { get; set; }

    public override string ToString()
    {
        return $"w={Width}&h={Height}";
    }
}