using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Ui;

public class CuddlerUi
{
    public CuddlerUi(IHtmlHelper htmlHelper)
    {
        HtmlHelper = htmlHelper;
    }

    public IHtmlHelper HtmlHelper { get; }
}