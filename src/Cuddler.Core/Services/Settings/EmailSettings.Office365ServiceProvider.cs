using System.ComponentModel;
using Cuddler.Data.Attributes;
using Cuddler.Data.Forms;

namespace Cuddler.Core.Services.Settings;

public class Office365ServiceProvider
{
    [ReadOnly(true)]
    [Row(0, 0)]
    [FormField(EFormField.Text)]
    public string? DisplayName { get; set; } = "coconcs support";

    [ReadOnly(true)]
    [Row(0, 0)]
    [FormField(EFormField.Text)]
    public string? Email { get; set; } = "support@cocooncs.com";

    [ReadOnly(true)]
    [Row(0, 0)]
    [FormField(EFormField.Password)]
    public string? AccessToken { get; set; }

    [ReadOnly(true)]
    [Row(1, 0)]
    public string? ExpiresIn { get; set; }

    [ReadOnly(true)]
    [FormField(EFormField.Hidden)]
    public string GraphClientId { get; set; } = "74e92b58-c0c7-40ec-877b-32ad70c79caa";

    [ReadOnly(true)]
    [FormField(EFormField.Hidden)]
    public string GraphClientSecret { get; set; } = "N1n8Q~R2IT6NNzJPlDY27_P5.-6H3.OQwZHJob0J";

    [ReadOnly(true)]
    [FormField(EFormField.Hidden)]
    public string GraphTenantId { get; set; } = "493ac592-fc60-4f75-bd3c-96b1df90b385";

    [FormField(EFormField.Office365Integration)]
    [ReadOnly(true)]
    [Row(2, 0)]
    public string? GraphUserId { get; set; } = "b5c1737a-32d9-4738-8c35-0b21cbc0bbae";

    [ReadOnly(true)]
    [Row(1, 0)]
    [FormField(EFormField.Password)]
    public string? RefreshToken { get; set; }

    [FormField(EFormField.Hidden)]
    public string Scopes { get; set; } = "offline_access user.read Mail.Send";


    //old
    //public string? GraphClientId { get; set; } = "051153eb-d016-430f-ad54-26e57610c929";

    //public string? GraphClientSecret { get; set; } = "hQkCy~.HFuFr5WqvCY_B..l~306l53v~Hb";

    //public string? GraphTenantId { get; set; } = "493ac592-fc60-4f75-bd3c-96b1df90b385";

    // [FormField(EFormField.Office365Integration)]
    //public string? GraphUserId { get; set; } = "b5c1737a-32d9-4738-8c35-0b21cbc0bbae";
}