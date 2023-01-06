using System.Collections;
using System.Text;

namespace Cuddler.Core.Email;

public static class EmailTemplateUtil
{
    public static string PrepareMessageBody(string emailBody, IDictionary replacements)
    {
        if (emailBody == null)
        {
            throw new ArgumentNullException(nameof(emailBody));
        }

        var itr = replacements.GetEnumerator();
        while (itr.MoveNext())
        {
            var oldValue = itr.Key?.ToString();
            var newValue = itr.Value ?? "N/A";
            if (oldValue != null)
            {
                emailBody = emailBody.Replace(oldValue, newValue.ToString());
            }
        }

        return emailBody;
    }

    public static string ReadEmbeddedTemplate(string template, IDictionary replacements)
    {
        return PrepareMessageBody(template, replacements);
    }

    public static async Task<string> ReadEmbeddedTemplate(Type rootType, string templateName, IDictionary replacements)
    {
        var template = await ReadEmbeddedTemplate(rootType, templateName);

        return PrepareMessageBody(template, replacements);
    }

    public static async Task<string> ReadEmbeddedTemplate(Type rootType, string templateName)
    {
        var templateKey = $"{rootType.Namespace}.{templateName}";
        var resourceStream = rootType.Assembly.GetManifestResourceStream(templateKey) ?? throw new NullReferenceException($"Template does not exist '{templateKey}'");
        using var reader = new StreamReader(resourceStream, Encoding.UTF8);

        return await reader.ReadToEndAsync();
    }
}