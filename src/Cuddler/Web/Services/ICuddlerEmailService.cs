using Cuddler.Data.Entities;
using Cuddler.Web.Models;

namespace Cuddler.Web.Services;

public interface ICuddlerEmailService
{
    void ArchiveEmail(string id);

    IQueryable<EmailEntity> ListArchivedEmails();

    IQueryable<EmailEntity> ListFailedEmails();

    IQueryable<EmailEntity> ListSentEmails();

    Task SaveEmail(EmailEntity emailEntity);

    Task SendEmailAsync(string id);

    Task SendEmailAsync(EmailEntity emailEntity, List<EmailAttachmentModel>? attachments = null);

    Task SendEmailOnlyAsync(EmailEntity emailEntity);

    Task SendEmailsAsync();

    void UnarchiveEmail(string id);
}