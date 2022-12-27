using Cuddler.Core.Services.EmailTemplates;
using Cuddler.Data.Entities;

namespace Cuddler.Core.Services.Emails;

public interface IEmailTemplateService
{
    Task<bool> CreateTemplate(EmailTemplateEntity entity);

    List<EmailTemplateEntity> GetActiveTemplates();

    List<EmailTemplateEntity> GetArchivedTemplates();

    EmailTemplateEntity GetTemplate(string id);

    EmailTemplateEntity GetTemplate<TTemplate>() where TTemplate : IEmailTemplate;

    bool HasMissingTemplates();

    List<EmailTemplate> ListEmailTemplates();

    List<EmailTemplate> ListMissingEmailTemplates();

    EmailTemplateEntity? ResetEmailTemplate(string id);
}