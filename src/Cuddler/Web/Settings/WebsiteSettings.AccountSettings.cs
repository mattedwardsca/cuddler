namespace Cuddler.Web.Settings;

public class AccountSettings
{
    public bool Enable2FA { get; set; }

    public bool EnableAuditorRole { get; set; }

    public bool EnableEmployeeAccountInvitations { get; set; }

    public bool EnableClientMembershipRequests { get; set; }

    public bool EnableAdvisorRole { get; set; }

    public bool EnableCoordinatorRole { get; set; }

    public bool EnableTimeZones { get; set; }

    public bool EnableLanguages { get; set; }

    public bool EnableSuperUser { get; set; }
}