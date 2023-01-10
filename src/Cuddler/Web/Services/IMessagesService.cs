using Cuddler.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Web.Services;

public interface IMessagesService
{
    List<MessageUserEntity> CreateMessages(MessageEntity entity, string[] accountIds, HttpRequest request);

    //Task<bool> CreateMessageTemplate(EmailTemplateEntity entity);

    //List<EmailTemplateEntity> GetActiveTemplates();

    //List<EmailTemplateEntity> GetArchivedTemplates();

    //EmailTemplateEntity GetTemplate(string id);

    //public List<EmailTemplateEntity> ListEmbeddedMessageTemplates();

    //EmailTemplateEntity ResetMessageTemplate(string id);
}