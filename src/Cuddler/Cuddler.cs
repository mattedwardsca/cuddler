using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler;

public class Cuddler
{
    public Cuddler(IHtmlHelper htmlHelper)
    {
        HtmlHelper = htmlHelper;
    }

    public IHtmlHelper HtmlHelper { get; }
}