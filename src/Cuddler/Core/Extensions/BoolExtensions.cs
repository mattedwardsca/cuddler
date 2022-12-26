using Microsoft.AspNetCore.Html;

// ReSharper disable once CheckNamespace
namespace BoostDC.Ui.Modules;

public static class BoolExtensions
{
    public static string IsFalse(this bool isFalse, string falseValue)
    {
        return !isFalse
            ? falseValue
            : string.Empty;
    }

    public static string IsFalse(this bool isFalse, string fValue, string tValue)
    {
        return !isFalse
            ? fValue
            : tValue;
    }

    public static HtmlString IsFalseHtml(this bool isFalse, string? falseValue)
    {
        return !isFalse
            ? new HtmlString(falseValue)
            : new HtmlString(string.Empty);
    }

    public static HtmlString IsFalseHtml(this bool isFalse, string fValue, string tValue)
    {
        return !isFalse
            ? new HtmlString(fValue)
            : new HtmlString(tValue);
    }

    public static string IsTrue(this bool isTrue, string trueValue)
    {
        return isTrue
            ? trueValue
            : string.Empty;
    }

    public static string IsTrue(this bool isTrue, string trueValue, string falseValue)
    {
        return isTrue
            ? trueValue
            : falseValue;
    }

    public static HtmlString IsTrueHtml(this bool isTrue, string trueValue)
    {
        return isTrue
            ? new HtmlString(trueValue)
            : new HtmlString(string.Empty);
    }

    public static HtmlString IsTrueHtml(this bool isTrue, string trueValue, string falseValue)
    {
        return isTrue
            ? new HtmlString(trueValue)
            : new HtmlString(falseValue);
    }
}