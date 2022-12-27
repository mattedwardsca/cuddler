using System.Globalization;

namespace Cuddler.Core.Utils;

public static class DateUtil
{
    public const string CanadianDateFormat = "MM/dd/yyyy";
    public const string CanadianDateTimeFormat = "MM/dd/yyyy HH:mm";
    public const string FormatCanadianDateString = "{0:MM/dd/yyyy}";
    public const string FormatCandianDateTimeString = "{0:MM/dd/yyyy HH:mm}";
    public const string FormatTimeString = "{0:HH:mm}";
    public const string TimeFormat = "HH:mm";
    private const int DaysInPeriod = 14;

    public static string[] ParseCanadianDateFormats =>
        new[]
        {
            CanadianDateFormat
        };

    public static bool AreEqual(DateTime date, DateTime date2)
    {
        var areEqual = date.Equals(date2);

        return areEqual;
    }

    // TODO Test
    public static string Checked(DateTime? dateTime)
    {
        return dateTime == null
            ? "checked='checked'"
            : "";
    }

    public static int CountDays(DateTime dtmStart, DateTime dtmEnd)
    {
        var startOfDay = GetStartOfDay(dtmEnd);
        var endOfDay = GetStartOfDay(dtmStart);
        var timeSpan = startOfDay - endOfDay;

        return (int)timeSpan.TotalDays + 1;
    }

    public static double CurrentTimeMills()
    {
        return (DateTime.UtcNow.ToLocalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }

    public static string GetDateDiffInWord(DateTime? comparedate)
    {
        return comparedate.HasValue == false
            ? string.Empty
            : GetDateDiffInWord(DateTime.UtcNow.ToLocalTime(), comparedate.Value);
    }

    public static string GetDateDiffInWord(DateTime now, DateTime comparedate)
    {
        string dateinWord;
        var tspDif = now - comparedate;
        var minutes = (int)tspDif.TotalMinutes;
        if (minutes <= 0)
        {
            dateinWord = tspDif.Seconds + " seconds ago";
        }
        else
        {
            if (minutes >= 525600)
            {
                dateinWord = $"{comparedate:MMMM yyyy}";
            }
            else
            {
                if (minutes >= 43200)
                {
                    var month = decimal.Divide(minutes, 43200);
                    if (month == 12)
                    {
                        dateinWord = "1 year ago";
                    }
                    else if (month == 1)
                    {
                        dateinWord = "1 month ago";
                    }
                    else
                    {
                        dateinWord = $"{month:0.#}" + " months ago";
                    }
                }
                else
                {
                    if (minutes >= 10080)
                    {
                        var i = decimal.Divide(minutes, 10080);
                        dateinWord = $"{i:0.#}" + " weeks ago";
                    }
                    else
                    {
                        if (minutes >= 24 * 60)
                        {
                            var i = decimal.Divide(minutes, 1440);
                            dateinWord = $"{i:0.#}" + " days ago";
                        }
                        else
                        {
                            if (minutes >= 90)
                            {
                                var i = decimal.Divide(minutes, 60);
                                dateinWord = $"{i:0.#}" + " hours ago";
                            }
                            else
                            {
                                dateinWord = minutes + " minutes ago";
                            }
                        }
                    }
                }
            }
        }

        return dateinWord;
    }

    public static IEnumerable<DateTime> GetDatesWithinMondayAndFriday(IEnumerable<DateTime> dates)
    {
        return from d in dates
               where d.DayOfWeek == DayOfWeek.Monday || d.DayOfWeek == DayOfWeek.Tuesday || d.DayOfWeek == DayOfWeek.Wednesday || d.DayOfWeek == DayOfWeek.Thursday || d.DayOfWeek == DayOfWeek.Friday
               select d;
    }

    public static IDictionary<string, DateTime> GetDays(DateTime startDate, DateTime endDate, bool descending)
    {
        var startYear = startDate.Year;
        var endYear = endDate.Year;
        var greaterYear = startYear <= endYear;
        if (!greaterYear)
        {
            throw new ArgumentException("End year must be greater than or equal to start year.");
        }

        var days = new Dictionary<string, DateTime>();

        // startDate = GetLastDayOfMonth(startDate);
        // endDate = GetLastDayOfMonth(endDate);
        if (descending)
        {
            while (endDate >= startDate)
            {
                days.Add($"{endDate:MMMM dd, yyyy}", Convert.ToDateTime(endDate.ToShortDateString()));
                endDate = endDate.AddDays(-1);
            }
        }
        else
        {
            while (startDate <= endDate)
            {
                days.Add($"{startDate:MMMM dd, yyyy}", Convert.ToDateTime(startDate.ToShortDateString()));
                startDate = startDate.AddDays(1);
            }
        }

        return days;
    }

    public static decimal GetDifferenceInHours(DateTime now, DateTime comparedate)
    {
        var tspDif = now - comparedate;
        var seconds = (int)tspDif.TotalSeconds;

        return SecondsUtil.ConvertSecondsToHours(seconds);
    }

    public static decimal GetDifferenceInMinutes(DateTime now, DateTime comparedate)
    {
        var tspDif = now - comparedate;
        var seconds = (int)tspDif.TotalSeconds;

        return (decimal)seconds / 60;
    }

    public static long GetDifferenceInSeconds(DateTime now, DateTime comparedate)
    {
        var tspDif = now - comparedate;
        var seconds = (long)tspDif.TotalSeconds;

        return seconds;
    }

    public static DateTime GetEndOfDay(DateTime date)
    {
        return new DateTime(date.Ticks - 1).AddDays(1);
    }

    public static DateTime GetFirstDayOfHalfYear(DateTime now)
    {
        if (now.Month <= 6)
        {
            return new DateTime(now.Year, 6, 1);
        }

        return new DateTime(now.Year, 12, 1);
    }

    public static DateTime GetFirstDayOfMonth(DateTime now)
    {
        return new DateTime(now.Year, now.Month, 1);
    }

    public static DateTime GetFirstDayOfQuarter(DateTime now)
    {
        if (now.Month <= 3)
        {
            return new DateTime(now.Year - 1, 1, 1);
        }

        if (now.Month <= 6)
        {
            return new DateTime(now.Year, 4, 1);
        }

        return now.Month <= 9
            ? new DateTime(now.Year, 7, 1)
            : new DateTime(now.Year, 10, 1);
    }

    public static DateTime GetFirstDayOfWeek(DateTime now)
    {
        var delta = DayOfWeek.Sunday - now.DayOfWeek;
        var firstDayOfWeek = now.AddDays(delta);

        return firstDayOfWeek;
    }

    public static DateTime GetFirstDayOfYear(DateTime now)
    {
        return new DateTime(now.Year, 1, 1);
    }

    public static double GetFractionalHours(int seconds)
    {
        var fractionalHours = (double)seconds / 3600;

        return Math.Round(fractionalHours, 2, MidpointRounding.ToEven);
    }

    public static string GetHalfYear(DateTime date)
    {
        string txtyear;
        if (date.Month <= 6)
        {
            txtyear = "1st Half, " + date.Year;
        }
        else
        {
            txtyear = "2st Half, " + date.Year;
        }

        return txtyear;
    }

    public static IDictionary<string, DateTime> GetHalfYears(int startYear, int endYear, bool descending)
    {
        if (startYear > endYear)
        {
            throw new ArgumentException("End year must be greater than start year.");
        }

        var years = new Dictionary<string, DateTime>();
        if (descending)
        {
            for (var year = endYear; year >= startYear; year--)
            {
                var y2 = new DateTime(year, 12, 31);
                years.Add(year + " - second half", y2);
                var y1 = new DateTime(year, 6, 30);
                years.Add(year + " - first half", y1);
            }
        }
        else
        {
            for (var year = startYear; year <= endYear; year++)
            {
                var y1 = new DateTime(year, 6, 30);
                years.Add(year + " - first half", y1);
                var y2 = new DateTime(year, 12, 31);
                years.Add(year + " - second half", y2);
            }
        }

        return years;
    }

    public static IDictionary<string, DateTime> GetHalfYearsbyDate(DateTime startDate, DateTime endDate, bool descending)
    {
        var startYear = startDate.Year;
        var endYear = endDate.Year;
        if (startYear > endYear)
        {
            throw new ArgumentException("End year must be greater than start year.");
        }

        var years = new Dictionary<string, DateTime>();
        startDate = GetLastDayOfHalfYear(startDate);
        endDate = GetLastDayOfHalfYear(endDate);
        if (descending)
        {
            while (endDate >= startDate)
            {
                if (endDate.Month <= 6)
                {
                    var y1 = new DateTime(endDate.Year, 6, 30);
                    years.Add(endDate.Year + " - first half", y1);
                }
                else
                {
                    var y2 = new DateTime(endDate.Year, 12, 31);
                    years.Add(endDate.Year + " - second half", y2);
                }

                endDate = endDate.AddMonths(-6);
                endDate = GetLastDayOfHalfYear(endDate);
            }
        }
        else
        {
            while (startDate <= endDate)
            {
                if (startDate.Month <= 6)
                {
                    var y1 = new DateTime(startDate.Year, 6, 30);
                    years.Add(startDate.Year + " - first half", y1);
                }
                else
                {
                    var y2 = new DateTime(startDate.Year, 12, 31);
                    years.Add(startDate.Year + " - second half", y2);
                }

                startDate = startDate.AddMonths(6);
                startDate = GetLastDayOfHalfYear(startDate);
            }
        }

        return years;
    }

    public static DateTime GetLastDayOfHalfYear(DateTime now)
    {
        if (now.Month <= 6)
        {
            return new DateTime(now.Year, 6, 30);
        }

        return new DateTime(now.Year, 12, 31);
    }

    public static DateTime GetLastDayOfMonth(DateTime today)
    {
        var endOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

        return GetEndOfDay(endOfMonth);
    }

    public static DateTime GetLastDayOfQuarter(DateTime now)
    {
        if (now.Month <= 3)
        {
            return new DateTime(now.Year, 3, 31);
        }

        if (now.Month <= 6)
        {
            return new DateTime(now.Year, 6, 30);
        }

        return now.Month <= 9
            ? new DateTime(now.Year, 9, 30)
            : new DateTime(now.Year, 12, 31);
    }

    public static DateTime GetLastDayOfWeek(in DateTime now)
    {
        return GetFirstDayOfWeek(now)
            .AddDays(7);
    }

    public static DateTime GetLastDayOfYear(DateTime now)
    {
        return new DateTime(now.Year, 12, 31);
    }

    public static DateTime GetMaxDate()
    {
        return new DateTime(9999, 12, 31);
    }

    public static int GetMinutes(string timeOfDay)
    {
        var time = DateTime.Parse(timeOfDay);

        return (int)time.TimeOfDay.TotalMinutes;
    }

    public static string? GetMonth(DateTime? startDate)
    {
        return startDate?.ToString("MMMM");
    }

    public static DateTime GetNextBusinessDay(DateTime dtmStart, int dayDiff)
    {
        var startOfDay = GetStartOfDay(dtmStart);
        var nextBusinessDay = startOfDay.AddDays(dayDiff);
        var day = (int)nextBusinessDay.DayOfWeek;
        if (day == 0)
        {
            nextBusinessDay = nextBusinessDay.AddDays(1);
        }

        if (day == 6)
        {
            nextBusinessDay = nextBusinessDay.AddDays(2);
        }

        return nextBusinessDay;
    }

    public static DateTime GetNextBusinessDay(DateTime dtmStart)
    {
        const int dayDiff = 1;
        var nextBusinessDay = dtmStart.AddDays(dayDiff);
        var day = (int)nextBusinessDay.DayOfWeek;
        if (day == 0)
        {
            nextBusinessDay = nextBusinessDay.AddDays(1);
        }

        if (day == 6)
        {
            nextBusinessDay = nextBusinessDay.AddDays(2);
        }

        return nextBusinessDay;
    }

    public static IEnumerable<DateTime> GetPayPeriodsInRange(DateTime start, DateTime end)
    {
        var epoch = new DateTime(2009, 11, 1);
        //var epoch = new DateTime(2009, 10, 31);
        var periodsTilStart = Math.Floor((start - epoch).TotalDays / DaysInPeriod);
        var next = epoch.AddDays(periodsTilStart * DaysInPeriod);
        if (next < start)
        {
            next = next.AddDays(DaysInPeriod);
        }

        while (next <= end)
        {
            yield return next;

            next = next.AddDays(DaysInPeriod);
        }
    }

    public static DateTime GetPayPeriodStartDate(DateTime givenDate)
    {
        var candidatePeriods = GetPayPeriodsInRange(givenDate.AddDays(-DaysInPeriod), givenDate.AddDays(DaysInPeriod));
        var period = from p in candidatePeriods
                     where p <= givenDate && givenDate < p.AddDays(DaysInPeriod)
                     select p;

        return period.First();
    }

    public static string GetQuarterOfYear(DateTime date)
    {
        var quater = date.Month / 3;
        var txtQuater = "Quarter " + quater + ", " + date.Year;

        return txtQuater;
    }

    public static IDictionary<string, DateTime> GetQuarters(int startYear, int endYear, bool descending)
    {
        var greaterYear = startYear <= endYear;
        if (!greaterYear)
        {
            throw new ArgumentException("End year must be greater than or equal to start year.");
        }

        var quarters = new Dictionary<string, DateTime>();
        if (descending)
        {
            for (var year = endYear; year >= startYear; year--)
            {
                var q4 = new DateTime(year, 12, 31);
                quarters.Add(year + " - Quarter 4", q4);
                var q3 = new DateTime(year, 9, 30);
                quarters.Add(year + " - Quarter 3", q3);
                var q2 = new DateTime(year, 6, 30);
                quarters.Add(year + " - Quarter 2", q2);
                var q1 = new DateTime(year, 3, 31);
                quarters.Add(year + " - Quarter 1", q1);
            }
        }
        else
        {
            for (var year = startYear; year <= endYear; year++)
            {
                var q1 = new DateTime(year, 3, 31);
                quarters.Add(year + " - Quarter 1", q1);
                var q2 = new DateTime(year, 6, 30);
                quarters.Add(year + " - Quarter 2", q2);
                var q3 = new DateTime(year, 9, 30);
                quarters.Add(year + " - Quarter 3", q3);
                var q4 = new DateTime(year, 12, 31);
                quarters.Add(year + " - Quarter 4", q4);
            }
        }

        return quarters;
    }

    public static IDictionary<string, DateTime> GetQuartersbyDate(DateTime startDate, DateTime endDate, bool descending)
    {
        var startYear = startDate.Year;
        var endYear = endDate.Year;
        var greaterYear = startYear <= endYear;
        if (!greaterYear)
        {
            throw new ArgumentException("End year must be greater than or equal to start year.");
        }

        startDate = GetLastDayOfQuarter(startDate);
        endDate = GetLastDayOfQuarter(endDate);
        var quarters = new Dictionary<string, DateTime>();
        if (descending)
        {
            while (endDate >= startDate)
            {
                var quater = endDate.Month / 3;
                quarters.Add(endDate.Year + " - Quarter " + quater, endDate);
                endDate = endDate.AddMonths(-3);
                endDate = GetLastDayOfQuarter(endDate);
            }
        }
        else
        {
            while (startDate <= endDate)
            {
                var quater = startDate.Month / 3;
                quarters.Add(startDate.Year + " - Quarter " + quater, startDate);
                //                    startDate = startDate.AddMonths(3);
                startDate = GetLastDayOfQuarter(endDate);
            }
        }

        return quarters;
    }

    public static DateTime GetStartOfDay(DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day);
    }

    public static DateTime GetStartOfDay(int hour)
    {
        var now = DateTime.UtcNow.ToLocalTime();
        var am = GetStartOfDay(now);
        var nowPlusHour = am.AddHours(hour);

        return nowPlusHour;
    }

    public static DateTime GetStartOfWeek(DateTime today)
    {
        var diff = (7 + (today.DayOfWeek - DayOfWeek.Sunday)) % 7;

        return today.AddDays(-1 * diff)
                    .Date;
    }

    public static int GetTimeFromString(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
        {
            return 0;
        }

        var hours = 0;
        int minutes;
        var time = timeString.Split(':');
        if (time.Length > 2)
        {
            throw new ArgumentException("Time can only have hours and minutes.");
        }

        if (time.Length > 1)
        {
            hours = time[0]
                .Length == 0
                ? 0
                : Convert.ToInt32(time[0]);

            minutes = time[1]
                .Length == 0
                ? 0
                : Convert.ToInt32(time[1]);
        }
        else
        {
            minutes = Convert.ToInt32(time[0]);
        }

        var seconds = hours * 3600 + minutes * 60;

        return seconds;
    }

    public static List<DateTime> GetWeekdayInRange(DateTime from, DateTime to, DayOfWeek day)
    {
        const int daysInWeek = 7;
        var result = new List<DateTime>();
        var daysToAdd = ((int)day - (int)from.DayOfWeek + daysInWeek) % daysInWeek;

        do
        {
            from = from.AddDays(daysToAdd);
            result.Add(from);
            daysToAdd = daysInWeek;
        } while (from < to);

        return result;
    }

    public static int GetWeekOfYear(in DateTime dateTime)
    {
        var myCi = new CultureInfo("en-US");
        var myCal = myCi.Calendar;
        var myCwr = myCi.DateTimeFormat.CalendarWeekRule;
        var myFirstDow = myCi.DateTimeFormat.FirstDayOfWeek;

        return myCal.GetWeekOfYear(dateTime, myCwr, myFirstDow);
    }

    public static IDictionary<string, DateTime> GetYears(int startYear, int endYear, bool descending)
    {
        var greaterYear = startYear <= endYear;
        if (!greaterYear)
        {
            throw new ArgumentException("End year must be greater than or equal to start year.");
        }

        var years = new Dictionary<string, DateTime>();
        if (descending)
        {
            for (var year = endYear; year >= startYear; year--)
            {
                var y1 = new DateTime(year, 12, 31);
                years.Add(year.ToString(), y1);
            }
        }
        else
        {
            for (var year = startYear; year <= endYear; year++)
            {
                var y1 = new DateTime(year, 12, 31);
                years.Add(year.ToString(), y1);
            }
        }

        return years;
    }

    public static string HasValue(object? obj)
    {
        return obj != null
            ? "Yes"
            : "No";
    }

    public static bool IsVailid(string date)
    {
        try
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Convert.ToDateTime(date);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsWeekend(DateTime startDate, DateTime endDate)
    {
        if (AreSameDay(startDate, endDate))
        {
            if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
        }

        if (Weekdays(startDate, endDate) > 0)
        {
            return false;
        }

        return true;
    }

    public static string JsonDateTimeRange(DateTime startDate, DateTime endDate)
    {
        var s = $"{startDate:MM/dd/yyyy}";
        var e = $"{endDate:MM/dd/yyyy}";

        return $"startDate={s}&endDate={e}";
    }

    public static string Length(string name, int length)
    {
        if (string.IsNullOrEmpty(name))
        {
            return string.Empty;
        }

        if (name.Length > length)
        {
            return name[..length] + "...";
        }

        return name;
    }

    public static List<int> ListDays(int month)
    {
        var months = new List<int>();
        var start = month;
        var numberofday = DateTime.DaysInMonth(DateTime.UtcNow.ToLocalTime()
                                                       .Year, start);
        while (start <= numberofday)
        {
            months.Add(start);
            start++;
        }

        return months;
    }

    public static IEnumerable<DateTime> ListFirstDayOfMonths(DateTime startDate, DateTime endDate)
    {
        var startYear = startDate.Year;
        var thisYear = endDate.Year;
        var thisMonth = endDate.Month;
        var totalYears = thisYear - startYear + 1;
        var changesPerYearAndMonth = (from year in Enumerable.Range(startYear, totalYears)
                                      from month in Enumerable.Range(1, 12)
                                      let key = new
                                      {
                                          Year = year,
                                          Month = month
                                      }
                                      select new
                                      {
                                          key.Year,
                                          key.Month
                                      }).ToList();

        var except = from c in changesPerYearAndMonth
                     where c.Year == thisYear && c.Month > thisMonth
                     select c;

        var timeperiods = changesPerYearAndMonth.Except(except);

        return timeperiods.Select(timeperiod => new DateTime(timeperiod.Year, timeperiod.Month, 1))
                          .ToList();
    }

    public static Dictionary<string, string> ListMonths()
    {
        var months = new Dictionary<string, string>();
        var start = 1;
        while (start <= 12)
        {
            var date = new DateTime(DateTime.UtcNow.ToLocalTime()
                                            .Year, start, 1);
            months.Add(date.ToString("MMMM"), date.Month.ToString());
            start++;
        }

        return months;
    }

    public static IEnumerable<DateTime> MonthsBetween(DateTime d0, DateTime d1)
    {
        return Enumerable.Range(0, (d1.Year - d0.Year) * 12 + (d1.Month - d0.Month) + 1)
                         .Select(m => new DateTime(d0.Year, d0.Month, 1).AddMonths(m));
    }

    public static DateTime ParseCanadianDate(string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return DateTime.ParseExact(value, CanadianDateFormat, CultureInfo.InvariantCulture);
    }

    public static DateTime? ParseCanadianDateSafe(string value)
    {
        return DateTime.ParseExact(value, CanadianDateFormat, CultureInfo.InvariantCulture);
    }

    public static DateTime? ParseCanadianDateTimeSafe(string value)
    {
        try
        {
            return DateTime.ParseExact(value, CanadianDateTimeFormat, CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            // ignored
        }

        return null;
    }

    public static DateTime? ParseDate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new DateTime?();
        }

        return DateTime.Parse(value, CultureInfo.InvariantCulture); //Invariant culture because if this runs on a Canadian server vs American server, this will completely change how it is parsed.
    }

    public static DateTime? ParseDatetime(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new DateTime?();
        }

        return DateTime.Parse(value, CultureInfo.InvariantCulture); //Invariant culture because if this runs on a Canadian server vs American server, this will completely change how it is parsed.
    }

    public static DateTime ParseJavascriptDate(string sDate, bool isStartDate)
    {
        if (sDate == null)
        {
            throw new ArgumentNullException(nameof(sDate));
        }

        sDate = sDate.Replace("%2F", "-");
        var parts = sDate.Split('-');
        var sYear = Convert.ToInt32(parts[0]);
        var sMonth = Convert.ToInt32(parts[1]);
        var sDay = Convert.ToInt32(parts[2]);
        if (isStartDate)
        {
            return new DateTime(sYear, sMonth, sDay, 0, 0, 0, 0);
        }

        return new DateTime(sYear, sMonth, sDay, 23, 59, 59, 59);
    }

    public static DateTime ParseShortDate(string sDate, bool isStartDate)
    {
        if (sDate == null)
        {
            throw new ArgumentNullException(nameof(sDate));
        }

        // sDate = sDate.Replace("%2F", "/");
        var parts = sDate.Split('/');
        var sYear = Convert.ToInt32(parts[2]);
        var sMonth = Convert.ToInt32(parts[0]);
        var sDay = Convert.ToInt32(parts[1]);
        if (isStartDate)
        {
            return new DateTime(sYear, sMonth, sDay, 0, 0, 0, 0);
        }

        return new DateTime(sYear, sMonth, sDay, 23, 59, 59, 59);
    }

    public static DateTime? ParseTimestamp(string? date)
    {
        if (date == null)
        {
            return null;
        }

        return DateTime.ParseExact(date, CanadianDateTimeFormat, CultureInfo.InvariantCulture);
    }

    public static string RelativeDate(DateTime date)
    {
        var timespan = DateTime.UtcNow.ToLocalTime()
                               .Subtract(date);
        if (timespan.Days > 1)
        {
            return date.ToString(CultureInfo.InvariantCulture);
        }

        if (timespan.Hours > 1)
        {
            return $"Over {timespan.Hours} hours ago.";
        }

        if (timespan.Minutes > 1)
        {
            return $"{timespan.Minutes} minutes ago.";
        }

        return $"{timespan.Seconds} seconds ago.";
    }

    public static int Weekdays(DateTime dtmStart, DateTime dtmEnd)
    {
        // This function includes the start and end date in the count if they fall on a weekday
        var dowStart = dtmStart.DayOfWeek == 0
            ? 7
            : (int)dtmStart.DayOfWeek;
        var dowEnd = dtmEnd.DayOfWeek == 0
            ? 7
            : (int)dtmEnd.DayOfWeek;
        var tSpan = dtmEnd - dtmStart;
        if (dowStart <= dowEnd)
        {
            return tSpan.Days / 7 * 5 + Math.Max(Math.Min(dowEnd + 1, 6) - dowStart, 0);
        }

        return tSpan.Days / 7 * 5 + Math.Min(dowEnd + 6 - Math.Min(dowStart, 6), 5);
    }

    public static IEnumerable<DateTime> YearsBetween(DateTime startDate, DateTime endDate)
    {
        DateTime iterator;
        DateTime limit;

        if (endDate > startDate)
        {
            iterator = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            limit = endDate;
        }
        else
        {
            iterator = new DateTime(endDate.Year, endDate.Month, startDate.Day);
            limit = startDate;
        }

        while (iterator <= limit)
        {
            yield return iterator;

            iterator = iterator.AddYears(1);
        }
    }

    private static bool AreSameDay(DateTime startDate, DateTime endDate)
    {
        return startDate.DayOfYear == endDate.DayOfYear && startDate.Year == endDate.Year;
    }
}