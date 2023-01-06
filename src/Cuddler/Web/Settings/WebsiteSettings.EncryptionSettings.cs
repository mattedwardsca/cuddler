using System.ComponentModel;
using Cuddler.Forms;
using Cuddler.Forms.Attributes;

namespace Cuddler.Web.Settings;

public class EncryptionSettings
{
    [Description("Warning: beta feature.")]
    public bool EncryptFiles { get; set; }

    [FormField(EFormField.Text)]
    [Description("Warning: beta feature.")]
    public string DocumentEncryptionKey { get; set; } = "02mCUiPNh50sM2rt2bYUnirF54zsjexN";
}