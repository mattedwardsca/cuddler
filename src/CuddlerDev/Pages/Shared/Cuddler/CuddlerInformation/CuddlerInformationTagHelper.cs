using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerInformation;

public class CuddlerInformationTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerInformationTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    public EFontAwesomeIcon Icon { get; set; } = EFontAwesomeIcon.None;

    public CuddlerUri? ButtonSubmitApi { get; set; }

    public string? ButtonText { get; set; }

    public EAlert Type { get; set; } = EAlert.Info;
}