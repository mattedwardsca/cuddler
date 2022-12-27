namespace Cuddler.Core.Services.Docs.Models;

public class ApiParameter
{
    public string ParameterType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool Required { get; set; }
}