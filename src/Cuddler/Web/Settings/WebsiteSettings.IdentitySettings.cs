namespace Cuddler.Web.Settings;

public class IdentitySettings
{
    public bool EnablePasswordRecovery { get; set; }

    public string? GoogleRecaptchaKey { get; set; }

    public bool RegistrationIsOpenToGuests { get; set; }
}