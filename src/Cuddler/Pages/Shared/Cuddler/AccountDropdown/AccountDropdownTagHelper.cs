using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Data.Entities;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.AccountDropdown;

public class AccountDropdownTagHelper : BaseTagHelper, ICuddler
{
    public AccountDropdownTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    [Required]
    public IAccount Account { get; set; } = null!;
}