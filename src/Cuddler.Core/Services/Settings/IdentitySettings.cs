namespace Cuddler.Core.Services.Settings;

public class IdentitySettings
{
    public bool DisablePasswordRecovery { get; set; }

    public string? GoogleRecaptchaKey { get; set; }

    public bool RegistrationIsOpenToGuests { get; set; } = true;
}