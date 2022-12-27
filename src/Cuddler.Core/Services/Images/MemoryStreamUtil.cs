namespace Cuddler.Core.Services.Images;

public static class MemoryStreamUtil
{
    public static bool SaveMemoryStreamToDisk(string filePath, MemoryStream formFile)
    {
        var file = new FileStream(filePath, FileMode.OpenOrCreate);
        formFile.WriteTo(file);
        file.Close();

        return true;
    }
}