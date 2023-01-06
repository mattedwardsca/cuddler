using System.ComponentModel;
using Cuddler.Forms.Attributes;

namespace Cuddler.Web.Settings;

public class CommonEmailSettings
{
    [DisplayName("Bcc Email")]
    [Description("Is there an additional email address you would like emails sent to without the recipient knowing? This is also known as bcc, or blind carbon copy.")]
    public string? BccEmail { get; set; }

    public string? EmailTemplateWrapper { get; set; }

    [Row(3, 0)]
    [DisplayName("Enable")]
    [Description("Allow the website to send administrative emails")]
    [DefaultValue(true)]
    public bool EnableEmail { get; set; } = true;
}