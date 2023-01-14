using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CuddlerDev.Pages.Blocks;

public class BlockModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {

        await Task.CompletedTask;

        return Page();
    }
}