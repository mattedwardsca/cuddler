using System.Linq.Expressions;
using Cuddler.Forms;
using Cuddler.Forms.Attributes;
using Cuddler.Utils;
using Cuddler.Web.Helpers;
using Kendo.Mvc.UI.Fluent;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cuddler.Web.Kendo;

public static class KendoGridExtensions
{
    private const string EditIcon = "<i class=\"fas fa-pen mr-2 eux-hidden-hover pointer\"></i>";


    public static GridBoundColumnBuilder<TType> ClientTemplate<TType>(this GridBoundColumnBuilder<TType> builder, EGridTemplate kendoGridTemplate, bool showEditIcon = false) where TType : class
    {
        var template = showEditIcon
            ? $"{GetGridTemplate(builder.Column.Member, kendoGridTemplate.ToString())} {EditIcon}"
            : $"{GetGridTemplate(builder.Column.Member, kendoGridTemplate.ToString())}";

        return builder.ClientTemplate(template);
    }

    public static GridBoundColumnBuilder<TType> ClientTemplate<TType>(this GridBoundColumnBuilder<TType> builder, EGridTemplate kendoGridTemplate, string value) where TType : class
    {
        var template = TemplateUtil.ClientTemplate(builder.Column.Member, kendoGridTemplate, value);

        return builder.ClientTemplate(template);
    }

    public static GridBoundColumnBuilder<TType> CuddlerTemplate<TType>(this GridBoundColumnBuilder<TType> builder, string headerName) where TType : class
    {
        var attribute = AssemblyScannerUtil.GetAttribute<GridTemplateAttribute>(typeof(TType), builder.Column.Member);
        var textName = attribute?.GridTemplate ?? nameof(EGridTemplate.Text);
        var template = GetGridTemplate(builder.Column.Member, textName);

        var gridBoundColumnBuilder = builder.ClientTemplate(template);
        gridBoundColumnBuilder.ClientHeaderTemplate(headerName);

        return gridBoundColumnBuilder;
    }

    // ReSharper disable once UnusedMethodReturnValue.Global
    public static GridBoundColumnBuilder<TType> DetailsLink<TType>(this GridBoundColumnBuilder<TType> builder, string href, string? icon = null) where TType : class
    {
        var newIcon = icon ?? TemplateUtil.ViewIcon;
        var template = $"<a href=\"{href}/#:{builder.Column.Member}#\">{newIcon}</a>";

        return builder.ClientHeaderTemplate(" ")
                      .Groupable(false)
                      .HtmlAttributes(TemplateUtil.TextCenter)
                      .Width(40)
                      .Exportable(false)
                      .Sortable(false)
                      .IncludeInMenu(false)
                      .Filterable(false)
                      .ClientTemplate(template);
    }

    public static string GetGridTemplate(string memberKey, string template)
    {
        string CreateKendoTemplate(Queue<string> queue, string? previous = null)
        {
            var dequeued = queue.Dequeue();
            var dequeueKey = previous == null
                ? dequeued
                : $"{previous}.{dequeued}";

            if (!queue.Any())
            {
                return $"#{TemplateUtil.KeyTemplate(dequeueKey, template)}#";
            }

            return $"if({dequeueKey}!==undefined && {dequeueKey}!=null){{ {CreateKendoTemplate(queue, dequeueKey)} }}";
        }

        var collection = memberKey.Split(".")
                                  .ToList();

        var kendoTemplate = $"#{CreateKendoTemplate(new Queue<string>(collection))}#";

        return kendoTemplate;
    }

    public static GridBoundColumnBuilder<TType> Link<TType>(this GridBoundColumnBuilder<TType> builder, string href, Expression<Func<TType, object?>> hrefIdProperty) where TType : class
    {
        var hrefKey = GetGridTemplate(hrefIdProperty, nameof(EGridTemplate.Text));

        var key = $"#={builder.Column.Member}#";

        var template = $"<a href=\"{href}/{hrefKey}\">{key}</a>";

        return builder.ClientTemplate(template);
    }

    // ReSharper disable once UnusedMethodReturnValue.Global
    public static GridBoundColumnBuilder<TType> Popup<TType>(this GridBoundColumnBuilder<TType> builder, string popupTitle, string detailsUrl, string popupId, EFontAwesomeIcon icon = EFontAwesomeIcon.EditRow) where TType : class
    {
        return builder.ClientHeaderTemplate(" ")
                      .ClientTemplate(PopupLink(popupTitle, detailsUrl, popupId, icon))
                      .Groupable(false)
                      .HtmlAttributes(TemplateUtil.TextCenter)
                      .Width(50)
                      .Exportable(false)
                      .Sortable(false)
                      .IncludeInMenu(false)
                      .Filterable(false);
    }

    // ReSharper disable once UnusedMethodReturnValue.Global
    public static GridTemplateColumnBuilder<TModel> Popup<TModel>(this GridColumnFactory<TModel> builder, string popupTitle, string detailsUrl, string popupId, EButtonType buttonType = EButtonType.Icon) where TModel : class
    {
        return builder.Template(PopupButton(popupTitle, detailsUrl, popupId, buttonType))
                      .HtmlAttributes(TemplateUtil.TextCenter)
                      .Exportable(false)
                      .IncludeInMenu(false);
    }

    public static string PopupButton(string popupTitle, string detailsLink, string popupId, EButtonType buttonType = EButtonType.Primary)
    {
        var buttonClass = EButtonTypeHelper.ToString(buttonType);

        return $"<span class=\"btn {buttonClass} d-block\" onclick=\"{popupId}_openGlobalPopup('{popupTitle}','{detailsLink}')\"><i class=\"fas fa-edit\"></i> {popupTitle} </span>";
    }

    private static string GetGridTemplate<TType>(Expression<Func<TType, object?>> property, string template)
    {
        var memberKey = GetMemberKey(property);
        var gridTemplate = GetGridTemplate(memberKey, template);

        return gridTemplate;
    }

    private static string GetMemberKey<TType>(Expression<Func<TType, object?>> property)
    {
        var propertyBody = property.Body.Print(); // ie. f.Payments.ChasePayment.ChaseApiKey
        propertyBody = propertyBody.Replace("(object)", string.Empty);
        var firstPart = propertyBody.Split('.')
                                    .First();

        var key = propertyBody[(firstPart.Length + 1)..];

        return key;
    }

    private static string PopupLink(string popupTitle, string detailsLink, string popupId, EFontAwesomeIcon icon)
    {
        return $"<span class=\"pointer\" onclick=\"{popupId}_openGlobalPopup('{popupTitle}','{detailsLink}/#:Id#')\"><i class=\"{EFontAwesomeIconHelper.ToString(icon)}\"></i></span>";
    }
}