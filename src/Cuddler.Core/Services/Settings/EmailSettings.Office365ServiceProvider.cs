using System.ComponentModel;
using Cuddler.Data.Attributes;
using Cuddler.Data.Forms;

namespace Cuddler.Core.Services.Settings;

public class Office365ServiceProvider
{
    [ReadOnly(true)]
    [Row(0, 0)]
    [FormField(EFormField.Text)]
    public string? DisplayName { get; set; } 

    [ReadOnly(true)]
    [Row(0, 0)]
    [FormField(EFormField.Text)]
    public string? Email { get; set; }

    [ReadOnly(true)]
    [Row(0, 0)]
    [FormField(EFormField.Password)]
    public string? AccessToken { get; set; }

    [ReadOnly(true)]
    [Row(1, 0)]
    public string? ExpiresIn { get; set; }

    [ReadOnly(true)]
    [FormField(EFormField.Hidden)]
    public string GraphClientId { get; set; } = null!;

    [ReadOnly(true)]
    [FormField(EFormField.Hidden)]
    public string GraphClientSecret { get; set; } = null!;

    [ReadOnly(true)]
    [FormField(EFormField.Hidden)]
    public string GraphTenantId { get; set; } = null!;

    [FormField(EFormField.Office365Integration)]
    [ReadOnly(true)]
    [Row(2, 0)]
    public string? GraphUserId { get; set; } = null!;

    [ReadOnly(true)]
    [Row(1, 0)]
    [FormField(EFormField.Password)]
    public string? RefreshToken { get; set; }

    [FormField(EFormField.Hidden)]
    public string Scopes { get; set; } = "offline_access user.read Mail.Send";
}