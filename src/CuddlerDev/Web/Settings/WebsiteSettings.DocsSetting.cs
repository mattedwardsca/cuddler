using CuddlerDev.Web.Docs;
using Newtonsoft.Json;

namespace CuddlerDev.Web.Settings;

public class DocsSetting
{
    public string? DocsJsonFile { get; set; }

    public string? DocsUrl { get; set; }

    public static DocUserManualDto? GetProjectDetail(string? json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }

        try
        {
            return JsonConvert.DeserializeObject<DocUserManualDto>(json);
        }
        catch (Exception)
        {
            return null;
        }
    }
}