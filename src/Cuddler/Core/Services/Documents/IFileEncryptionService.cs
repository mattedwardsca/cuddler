namespace Cuddler.Core.Services.Documents;

public interface IFileEncryptionService
{
    MemoryStream LoadFromDisk(string inputFilePath, bool encrypt);

    Task SaveToDisk(Stream stream, string outputfilePath, bool encrypt);

    Task<MemoryStream> EncryptStream(Stream stream);
}