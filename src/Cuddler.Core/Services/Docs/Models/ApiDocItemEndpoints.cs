namespace Cuddler.Core.Services.Docs.Models;

public class ApiDocItemEndpoints
{
    public IEnumerable<ApiParameter> Parameters { get; set; } = new List<ApiParameter>();

    public string Name { get; set; } = null!;

    public string ApiUrl { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string? Description { get; set; }
}