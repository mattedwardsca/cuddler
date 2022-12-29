using System.ComponentModel;
using Cuddler.Forms;
using Cuddler.Forms.Attributes;

namespace Cuddler.Settings;

public class EncryptionKeysSettings
{
    [ReadOnly(true)]
    [FormField(EFormField.Text)]
    public string DocumentEncryptionKey { get; set; } = "02mCUiPNh50sM2rt2bYUnirF54zsjexN";
}