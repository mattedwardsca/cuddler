using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Modules;

public abstract class AppBase : IApp
{
    private string? _id;

    public bool IsForOrganizations { get; set; }

    public string Segment => Name.Replace(" ", string.Empty);

    public bool Hidden { get; set; }

    public string? Icon { get; set; }

    public string Id
    {
        get => _id ?? ConvertToMd5HashGuid(Name);
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

    private static string ConvertToMd5HashGuid(string? value)
    {
        // convert null to empty string - null can not be hashed
        value ??= string.Empty;

        // get the byte representation
        var bytes = Encoding.Default.GetBytes(value);

        // create the md5 hash
        var md5Hasher = MD5.Create();
        var data = md5Hasher.ComputeHash(bytes);

        // convert the hash to a Guid
        return new Guid(data).ToString();
    }
}