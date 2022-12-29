namespace Cuddler.Configuration.Language;

public class LanguageOption
{
    public LanguageOption(string id, string code, string description, string symbol)
    {
        Id = id;
        Code = code;
        Description = description;
        Symbol = symbol;
    }

    public string Code { get; set; }

    public string? Description { get; set; }

    public string Id { get; set; }

    public string Symbol { get; set; }
}