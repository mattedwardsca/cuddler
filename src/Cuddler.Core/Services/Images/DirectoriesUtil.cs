namespace Cuddler.Core.Services.Images;

public static class DirectoriesUtil
{
    public static string CreateDocumentFolder(string rootFolder, string documentId)
    {
        if (string.IsNullOrEmpty(rootFolder))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(rootFolder));
        }

        if (string.IsNullOrEmpty(documentId))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(documentId));
        }

        var fileFolder = $"{rootFolder}\\{documentId}\\";
        if (!Directory.Exists(fileFolder))
        {
            Directory.CreateDirectory(fileFolder);
        }

        return fileFolder;
    }
}