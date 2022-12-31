using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Forms.Ui;

public static class CuddlerExtensions
{
    public static CuddlerUi CuddlerUi(this IHtmlHelper htmlHelper)
    {
        return new CuddlerUi(htmlHelper);
    }
}