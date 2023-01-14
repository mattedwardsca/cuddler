using Microsoft.AspNetCore.Http;

namespace CuddlerDev.Utils;

public static class RequestDatesUtil
{
    public static string FormatQueryDates(DateTime? startDate, DateTime? enddate)
    {
        return $"{FormatQueryDate("start", startDate)}&{FormatQueryDate("end", enddate)}";
    }

    public static string GetDateRangeParameters(HttpRequest httpRequest)
    {
        return RequestDateRange(httpRequest);
    }

    public static DateTime? GetEndDate(HttpRequest httpRequest)
    {
        return RequestEndDate(httpRequest.HttpContext.Request, false);
    }

    public static string GetOptionalStartDateParameter(HttpRequest request)
    {
        var startdate = GetStartDate(request);

        return FormatQueryDate("start", startdate);
    }

    public static string GetRequiredDateRangeParameters(HttpRequest httpRequest)
    {
        return RequestDateRange(httpRequest, true);
    }

    public static DateTime GetRequiredEndDate(HttpRequest httpRequest)
    {
        return (DateTime)RequestEndDate(httpRequest, true)!;
    }

    public static DateTime GetRequiredStartDate(HttpRequest httpRequest)
    {
        return (DateTime)RequestStartDate(httpRequest, true)!;
    }

    public static DateTime? GetStartDate(HttpRequest httpRequest)
    {
        return RequestStartDate(httpRequest, false);
    }

    public static string RequestDateRange(HttpRequest request, bool required = false)
    {
        var startdate = required
            ? GetRequiredStartDate(request)
            : GetStartDate(request);
        var enddate = required
            ? GetRequiredEndDate(request)
            : GetEndDate(request);

        var startStr = startdate == null
            ? string.Empty
            : FormatQueryDate("start", startdate);

        var endStr = enddate == null
            ? string.Empty
            : FormatQueryDate("end", enddate);

        if (string.IsNullOrEmpty(endStr))
        {
            return startStr;
        }

        if (string.IsNullOrEmpty(startStr))
        {
            return endStr;
        }

        return $"{startStr}&{endStr}";
    }

    public static DateTime? RequestEndDate(HttpRequest request, bool required)
    {
        if (!DateTime.TryParse(request.Query["end"]
                                      .FirstOrDefault(), out var enddate))
        {
            if (required)
            {
                enddate = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                return null;
            }
        }

        return new DateTime(enddate.Ticks - 1).AddDays(1);
    }

    public static DateTime? RequestStartDate(HttpRequest request, bool required)
    {
        if (!DateTime.TryParse(request.Query["start"]
                                      .FirstOrDefault(), out var startdate))
        {
            if (required)
            {
                startdate = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                return null;
            }
        }

        return startdate.Date;
    }

    public static string StartDateRangeParameter(HttpRequest request)
    {
        var startdate = GetRequiredStartDate(request);

        return FormatQueryDate("start", startdate);
    }

    private static string FormatQueryDate(string paramName, DateTime? enddate)
    {
        if (enddate == null)
        {
            return string.Empty;
        }

        return $"{paramName}={enddate.Value.Month}/{enddate.Value.Day}/{enddate.Value.Year}";
    }
}