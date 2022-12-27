using Cuddler.Core.Services.Modules.Models;
using Cuddler.Core.Utils;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Web.Modules;

public abstract class AppBase : IClientApp
{
    private string? _id;

    public bool IsForOrganizations { get; set; }

    public string Segment => Name.Replace(" ", string.Empty);

    public bool Hidden { get; set; }

    public string? Icon { get; set; }

    public string Id
    {
        get => _id ?? WebIdUtil.GetGuidId(Name);
        set => _id = value;
    }

    public bool IsForAdmins { get; set; }

    public bool IsForClients { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public abstract Task<List<IMenuItem>> GetAppMenu(HttpContext context);

    public async Task<List<IMenuItem>> GetAppMenuLinks(HttpContext context)
    {
        var menu = await GetAppMenu(context);
        foreach (var item in menu)
        {
            item.Hide = true;
        }

        return menu.Where(w => w.LinkType == ELinkType.Link && !w.Hide)
                   .ToList();
    }

    public override string ToString()
    {
        return $"/{Segment}";
    }
}