namespace Cuddler.Web.Models;

public class EmailAttachmentModel
{
    public int? Size { get; set; }

    public byte[]? ContentBytes { get; set; }

    public string? ContentType { get; set; }

    public string? Name { get; set; }

    public string? ContentId { get; set; }

    public string? Id { get; set; }
}