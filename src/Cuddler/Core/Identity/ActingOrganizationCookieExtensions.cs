using System.Text;
using Cuddler.Core.Context;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Core.Identity;

public static class ActingOrganizationCookieExtensions
{
    public const string ActingOrganizationCookieName = "d27691f0-66b1-4a04-8d30-42f204e386fe";

    public static IOrganization GetActingOrganization(this HttpContext httpContext)
    {
        var loggedInAccount = httpContext.GetLoggedInAccount();
        if (!loggedInAccount.IsAdmin)
        {
            return loggedInAccount.GetProfile()
                                  .GetOrganization()
                   ?? throw new InvalidOperationException("User does not belong to an organization. Error: be5606fb-3a3a-4c1f-94d8-627cc117906c");
        }

        var actingOrganizationId = GetString(httpContext.Session, ActingOrganizationCookieName);

        if (string.IsNullOrEmpty(actingOrganizationId))
        {
            return loggedInAccount.GetProfile()
                                  .GetOrganization()!;
        }

        var repository = httpContext.GetService<IRepository>();
        var organization = (IOrganization)repository.DbSet("OrganizationEntity")!.Single(w => w.Id == actingOrganizationId);

        return organization;
    }

    public static void RemoveActingOrganization(this HttpResponse response, string organizationId)
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(-1)
        };

        response.Cookies.Append(ActingOrganizationCookieName, organizationId, cookieOptions);
    }

    public static void SetActingOrganization(this HttpResponse response, string organizationId)
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(1)
        };

        response.Cookies.Append(ActingOrganizationCookieName, organizationId, cookieOptions);
    }

    private static string? GetString(ISession session, string key)
    {
        var data = session.Get(key);
        if (data == null)
        {
            return null;
        }

        return Encoding.UTF8.GetString(data);
    }
}