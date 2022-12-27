using BoostDC.Libs.SendEmail.Shared;
using Cuddler.Data.Entities;

namespace Cuddler.Core.Services.Emails;

public interface IBoostEmailService
{
    void ArchiveEmail(string id);

    IQueryable<EmailEntity> ListArchivedEmails();

    IQueryable<EmailEntity> ListFailedEmails();

    IQueryable<EmailEntity> ListSentEmails();

    Task SaveEmail(EmailEntity emailEntity);

    Task SendEmailAsync(string id);

    Task SendEmailAsync(EmailEntity emailEntity, List<EmailAttachment>? attachments = null);

    Task SendEmailOnlyAsync(EmailEntity emailEntity);

    Task SendEmailsAsync();

    void UnarchiveEmail(string id);
}