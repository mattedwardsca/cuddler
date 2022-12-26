using System.Text.Encodings.Web;
using Cuddler.Core.Data;
using Cuddler.Pages.Shared.Cuddler.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.ViewModels;

public sealed class ColumModel
{
    public List<BaseTagHelper> Tags { get; } = new();
}

public sealed class ColumnBuilder
{
    private readonly HtmlEncoder _htmlEncoder;
    private readonly IHtmlHelper _htmlHelper;
    private readonly RowModel _row;

    public ColumnBuilder(RowModel row, IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
    {
        _row = row;
        _htmlHelper = htmlHelper;
        _htmlEncoder = htmlEncoder;
    }

    public void Column(Action<TagBuilder> t)
    {
        var col = new ColumModel();
        _row.Cols.Add(col);

        var model = new TagBuilder(col, _htmlHelper, _htmlEncoder);
        t.Invoke(model);
    }
}

public sealed class RowBuilder
{
    public List<ColumnBuilder> Columns { get; } = new();
}

public sealed class TagBuilder
{
    private readonly ColumModel _col;
    private readonly HtmlEncoder _htmlEncoder;
    private readonly IHtmlHelper _htmlHelper;

    public TagBuilder(ColumModel col, IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
    {
        _col = col;
        _htmlHelper = htmlHelper;
        _htmlEncoder = htmlEncoder;
    }

    public void Tag<T>(Action<T> p) where T : BaseTagHelper
    {
        var tag = (T)Activator.CreateInstance(typeof(T), _htmlHelper, _htmlEncoder)!;
        _col.Tags.Add(tag);

        p.Invoke(tag);
    }
}

public abstract class ViewModel
{
    private readonly HtmlEncoder _htmlEncoder;
    protected readonly IHtmlHelper HtmlHelper;

    protected ViewModel(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
    {
        HtmlHelper = htmlHelper;
        _htmlEncoder = htmlEncoder;
    }

    public List<RowModel> Rows { get; } = new();

    public ECuddlerTemplate Template { get; set; }

    public static T CreateInstance<T>(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) where T : ViewModel
    {
        return (T)Activator.CreateInstance(typeof(T), htmlHelper, htmlEncoder)!;
    }

    public abstract Task InitAsync();

    public void Row(Action<ColumnBuilder> c)
    {
        var row = new RowModel();
        Rows.Add(row);

        var model = new ColumnBuilder(row, HtmlHelper, _htmlEncoder);
        c.Invoke(model);
    }
}

public sealed class RowModel
{
    public List<ColumModel> Cols { get; } = new();

    public ELayout PageLayout { get; set; } = ELayout.Block;
}