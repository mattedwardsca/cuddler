using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Forms.Ui;
using Cuddler.Web.Api;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerInformation;

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