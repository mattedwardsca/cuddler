namespace CuddlerDev.Core.Email;

public interface IEmailTemplate
{
    string Category { get; set; }

    string Content { get; set; }

    string Id { get; set; }

    string Purpose { get; set; }

    string Subject { get; set; }
}