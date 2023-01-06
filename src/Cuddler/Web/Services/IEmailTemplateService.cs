using Cuddler.Data.Entities;

namespace Cuddler.Web.Services;

public interface IEmailTemplateService
{
    EmailTemplateEntity GetTemplate(string id);

    bool HasMissingTemplates();

    void ImportMissingEmailTemplates();

    List<EmailTemplateEntity> ListAllEmailTemplates();

    void ResetAllEmailTemplates();

    bool ResetEmailTemplate(string id);
}