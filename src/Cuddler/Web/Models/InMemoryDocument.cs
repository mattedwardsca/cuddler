using Cuddler.Data.Entities;

namespace Cuddler.Web.Models;

public class InMemoryDocument
{
    public InMemoryDocument(DocumentEntity document, byte[] fileBytes)
    {
        Document = document;
        FileBytes = fileBytes;
    }

    public DocumentEntity Document { get; set; }

    public byte[] FileBytes { get; }
}