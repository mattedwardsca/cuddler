using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using Cuddler.Data.Context;
using Cuddler.Data.Entities;
using Cuddler.Forms;
using Cuddler.Forms.Ui;
using Cuddler.Helpers;
using Cuddler.Pages.Shared.Cuddler.ActionMenu;
using Cuddler.Pages.Shared.Cuddler.CuddlerReplace;
using Cuddler.Pages.Shared.Cuddler.DynamicForm;
using Cuddler.Web.Utils;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Web.Dynamic;

public class DynamicFormWrapper<TData, TService> where TData : class, IData where TService : class, IService
{
    private readonly HttpContext _httpContext;
    private Action<DynamicQueryBuilder<TData>> _queryAction = null!;
    private Action<DynamicFormPropertyFactory<TData>> _schemaAction = null!;

    public DynamicFormWrapper(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    public async Task<IHtmlContent> Create(IHtmlHelper htmlHelper, TData? data = null)
    {
        data ??= Default();
        var formFields = new List<FormField>();
        _schemaAction(new DynamicFormPropertyFactory<TData>(formFields));
        var tag = new DynamicFormTagHelper(htmlHelper, HtmlEncoder.Default);
        tag.Fields = GetPropertyList(formFields, data);
        tag.PostUrl = _httpContext.Request.CombinedPath();
        tag.Handler = EDynamicHandler.Create;

        return await htmlHelper.PartialAsync("Cuddler/DynamicForm/Default", tag);
    }

    public async Task<IHtmlContent> Delete(IHtmlHelper htmlHelper)
    {
        var data = Single(_queryAction);
        var formFields = new List<FormField>();
        _schemaAction(new DynamicFormPropertyFactory<TData>(formFields));
        var tag = new DynamicFormTagHelper(htmlHelper, HtmlEncoder.Default);
        tag.Fields = GetPropertyList(formFields, data);
        tag.PostUrl = _httpContext.Request.CombinedPath();
        tag.Handler = EDynamicHandler.Delete;
        tag.ReadOnly = true;

        return await htmlHelper.PartialAsync("Cuddler/DynamicForm/Default", tag);
    }

    public async Task<ActionMenuItems> GetActionMenu(IHtmlHelper html, EDynamicHandler handler, string baseUrl, string blockId)
    {
        CuddlerReplaceTagHelper ReplaceLink(EDynamicHandler linkHandler)
        {
            EFontAwesomeIcon icon;
            switch (linkHandler)
            {
                case EDynamicHandler.Restore:
                    icon = EFontAwesomeIcon.TrashRestore;
                    break;

                case EDynamicHandler.Update:
                    icon = EFontAwesomeIcon.Edit;
                    break;

                case EDynamicHandler.Delete:
                    icon = EFontAwesomeIcon.Trash;
                    break;

                case EDynamicHandler.Create:
                    icon = EFontAwesomeIcon.Plus;
                    break;

                case EDynamicHandler.View:
                    icon = EFontAwesomeIcon.EditRow;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(linkHandler), linkHandler, null);
            }

            var popupEditorTagHelper = new CuddlerReplaceTagHelper(html, HtmlEncoder.Default)
            {
                Url = baseUrl,
                Handler = linkHandler,
                Icon = icon,
                Label = linkHandler.ToString(),
                BlockId = blockId
            };
            return popupEditorTagHelper;
        }

        var menuItems = new ActionMenuItems(html);
        await menuItems.Add(await html.CuddlerUi()
                                      .Template(ReplaceLink(EDynamicHandler.Create)));
        await menuItems.Add(await html.CuddlerUi()
                                      .Template(ReplaceLink(EDynamicHandler.Update)));
        await menuItems.Add(await html.CuddlerUi()
                                      .Template(ReplaceLink(EDynamicHandler.Delete)));
        await menuItems.Add(await html.CuddlerUi()
                                      .Template(ReplaceLink(EDynamicHandler.Restore)));
        await menuItems.Add(await html.CuddlerUi()
                                      .Template(ReplaceLink(EDynamicHandler.View)));
        return menuItems;
    }

    public DynamicFormWrapper<TData, TService> Query(Action<DynamicQueryBuilder<TData>> queryAction)
    {
        _queryAction = queryAction;

        return this;
    }

    public async Task<IHtmlContent> Restore(IHtmlHelper htmlHelper)
    {
        var data = Single(_queryAction);
        var formFields = new List<FormField>();
        _schemaAction(new DynamicFormPropertyFactory<TData>(formFields));
        var tag = new DynamicFormTagHelper(htmlHelper, HtmlEncoder.Default);
        tag.Fields = GetPropertyList(formFields, data);
        tag.Handler = EDynamicHandler.Restore;
        tag.ReadOnly = true;

        return await htmlHelper.PartialAsync("Cuddler/DynamicForm/Default", tag);
    }

    public DynamicFormWrapper<TData, TService> Schema(Action<DynamicFormPropertyFactory<TData>> schemaAction)
    {
        _schemaAction = schemaAction;

        return this;
    }

    public async Task<DynamicActionResult<TData>> Service(Expression<Func<TService, Task<DynamicActionResult<TData>>>> func)
    {
        var service = _httpContext.GetService<TService>();
        var data = await func.Compile()
                             .Invoke(service);

        await Task.CompletedTask;

        return data;
    }

    public async Task<IHtmlContent> Show(IHtmlHelper htmlHelper, EDynamicHandler handler)
    {
        return handler switch
        {
            EDynamicHandler.Restore => await Restore(htmlHelper),
            EDynamicHandler.Update => await Update(htmlHelper),
            EDynamicHandler.Delete => await Delete(htmlHelper),
            EDynamicHandler.Create => await Create(htmlHelper),
            EDynamicHandler.View => await View(htmlHelper),
            _ => throw new ArgumentOutOfRangeException(nameof(handler), handler, null)
        };
    }

    public async Task<IHtmlContent> Update(IHtmlHelper htmlHelper)
    {
        var data = Single(_queryAction);
        var formFields = new List<FormField>();
        _schemaAction(new DynamicFormPropertyFactory<TData>(formFields));
        var tag = new DynamicFormTagHelper(htmlHelper, HtmlEncoder.Default);
        tag.Fields = GetPropertyList(formFields, data);
        tag.PostUrl = _httpContext.Request.CombinedPath();
        tag.Handler = EDynamicHandler.Update;

        return await htmlHelper.PartialAsync("Cuddler/DynamicForm/Default", tag);
    }

    public async Task<IHtmlContent> View(IHtmlHelper htmlHelper)
    {
        var data = Single(_queryAction);
        var formFields = new List<FormField>();
        _schemaAction(new DynamicFormPropertyFactory<TData>(formFields));
        var tag = new DynamicFormTagHelper(htmlHelper, HtmlEncoder.Default);
        tag.Fields = GetPropertyList(formFields, data);
        tag.Handler = EDynamicHandler.View;
        tag.ReadOnly = true;
        tag.IsView = true;

        return await htmlHelper.PartialAsync("Cuddler/DynamicForm/View", tag);
    }

    protected TData Default()
    {
        var data = Activator.CreateInstance<TData>();
        return data;
    }

    protected TData Single(Action<DynamicQueryBuilder<TData>> action)
    {
        var _dynamicQueryBuilder = new DynamicQueryBuilder<TData>();
        action(_dynamicQueryBuilder);

        var data = GetQueryable(_dynamicQueryBuilder.AsQueryObj()
                                                    .ToStringFilter());

        return data.Single();
    }

    private static string? GetPropertyAsString(object obj, string propertyName)
    {
        var result = obj.GetType()
                        .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        ?.GetMethod?.Invoke(obj, Array.Empty<object>());

        return result?.ToString();
    }

    private static List<FormField> GetPropertyList(List<FormField> Properties, TData? Model)
    {
        if (Model != null)
        {
            foreach (var inputProperty in Properties)
            {
                var inputPropertyValue = GetPropertyAsString(Model, inputProperty.Name);
                inputProperty.Value = inputPropertyValue ?? inputProperty.DefaultValue?.ToString();
            }
        }

        return Properties.OrderBy(o => o.Row)
                         .ThenBy(o => o.Col)
                         .ToList();
    }

    private IEnumerable<TData> GetQueryable(string q)
    {
        var repository = _httpContext.GetService<IRepository>();

        var ds = new DataSourceRequest();
        ds.Filters = FilterDescriptorFactory.Create(q);

        var queryable = repository.DbSet(typeof(TData));
        if (queryable == null)
        {
            throw new FileNotFoundException($"Cuddler API {typeof(TData).Name} does not exist");
        }

        var request = new DataSourceRequest();
        request.Filters = FilterDescriptorFactory.Create(q);
        var data = queryable.ToDataSourceResult(request)
                            .Data.Cast<TData>();

        return data;
    }
}