namespace Cuddler.Web.Blocks;

public class BlockModel
{
    public List<TemplatesInputModel> Inputs { get; set; } = null!;

    public string SubmitApiUrl { get; set; } = null!;
}