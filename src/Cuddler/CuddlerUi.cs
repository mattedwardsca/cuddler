using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler;

public class CuddlerUi
{
    public CuddlerUi(IHtmlHelper htmlHelper)
    {
        HtmlHelper = htmlHelper;
    }

    public IHtmlHelper HtmlHelper { get; }
}