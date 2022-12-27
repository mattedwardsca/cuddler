using System.ComponentModel.DataAnnotations;

namespace Cuddler.Core.Services.EmailTemplates;

/// <summary>
///     EmailTemplate has the same signature as IEmailTemplate but does not implement IEmailTemplate so that the base class
///     doesn't show up in a library scan.
/// </summary>
public abstract class EmailTemplate
{
    protected EmailTemplate(string id, string purpose, string subject, string content, string category)
    {
        Subject = subject;
        Id = id;
        Content = content;
        Category = category;
        Purpose = purpose;
    }

    [Required]
    public string Category { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public string Id { get; set; }

    [Required]
    public string Purpose { get; set; }

    [Required]
    public string Subject { get; set; }
}