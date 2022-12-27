using Cuddler.Web.Helpers;

namespace Cuddler.Core.Utils;

public static class BootstrapUtil
{
    public static string ToString(EAlert? alert)
    {
        if (alert == null)
        {
            return string.Empty;
        }

        return alert switch
        {
            EAlert.Success => "alert-success",
            EAlert.Info => "alert-info",
            EAlert.Warning => "alert-warning",
            EAlert.Danger => "alert-danger",
            EAlert.Meta => "alert-meta",
            var _ => Enum.GetName(typeof(EAlert), alert) ?? "alert-info"
        };
    }
}