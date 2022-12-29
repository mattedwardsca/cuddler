namespace Cuddler.Configuration;

public class CultureModel
{
    public CultureModel(string id, string name, string description, string symbol)
    {
        Name = name;
        Description = description;
        Symbol = symbol;
        Id = id;
    }

    public string Description { get; set; }

    public string Id { get; set; }

    public string Name { get; set; }

    public string Symbol { get; set; }
}