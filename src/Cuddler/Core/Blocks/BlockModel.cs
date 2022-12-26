namespace Cuddler.Core.Blocks;

public class BlockModel
{
    public List<TemplatesInputModel> Inputs { get; set; } = null!;

    public string SubmitApiUrl { get; set; } = null!;
}