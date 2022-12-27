namespace Cuddler.Core.Services.Images;

public class ExtensionUtil
{
    public static string GetExtension(string? fileName)
    {
        var extension = Path.GetExtension(fileName);
        extension = extension?.Replace('.', ' ')
                             .Trim() ?? "";

        return extension.ToLower();
    }

    public static EImageType GetFileType(string extension)
    {
        if (extension.EndsWith("png", StringComparison.InvariantCultureIgnoreCase))
        {
            return EImageType.Png;
        }

        if (extension.StartsWith("jp", StringComparison.InvariantCultureIgnoreCase))
        {
            return EImageType.Jpeg;
        }

        if (extension.Equals("svg", StringComparison.InvariantCultureIgnoreCase))
        {
            return EImageType.Svg;
        }

        throw new NotImplementedException($"Extension '{extension}' not supported");
    }
}