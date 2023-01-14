using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.ActionButton;

public class ActionButtonTagHelper : BaseTagHelper, ICuddler
{
    public ActionButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public EActionComplete ActionComplete { get; set; }

    [Required]
    public EButtonType ButtonType { get; set; }

    public bool JumboSize { get; set; }

    public bool Confirm { get; set; }

    public string ConfirmText { get; set; } = "Remove item?";

    public string? RedirectUrl { get; set; }

    public CuddlerUri? SubmitApi { get; set; }
}