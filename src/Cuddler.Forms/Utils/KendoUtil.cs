using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cuddler.Forms.Utils;

public static class KendoUtil
{
    public const string ViewIcon = "<i class=\"fas fa-edit font-size-16\"></i>";
    public const string DeleteButton = "<button type=\"button\" class=\"btn btn-icon btn-icon-danger p-inherit k-grid-delete\" tabindex=\"-1\"><span><i class=\"fas fa-trash fa-fw\"></i></span></button>";

    public static object TextCenter => new
    {
        @class = "text-center"
    };

    public static object TextRight => new
    {
        @class = "text-right"
    };

    public static object TextBold => new
    {
        @class = "text-bold"
    };

    public static object Width100 => new
    {
        style = "width:100%;"
    };

    public static IHtmlContent EditLink(string href)
    {
        return new HtmlString($"<a href=\"{href}\">{ViewIcon}</a>");
    }

    public static string GetKey<TType>(Expression<Func<TType, object?>> property)
    {
        var propertyBody = property.Body.Print(); // ie. f.Payments.ChasePayment.ChaseApiKey
        propertyBody = propertyBody.Replace("(object)", string.Empty);
        var firstPart = propertyBody.Split('.')
                                    .First();

        var key = propertyBody[(firstPart.Length + 1)..];

        return key;
    }
}