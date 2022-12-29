namespace Cuddler.Core.Language;

public static class LanguageUtil
{
    public static CultureModel Chinese()
    {
        return new CultureModel("58f2e272-58b6-44f5-a478-2fd1c41aec57", LanguageCodes.ChinesePrc, "Chinese", "简");
    }

    public static string ConvertCodeToDescription(string name)
    {
        CultureModel? model = null;
        foreach (var l in ListSupportedLanguages())
        {
            if (l.Name == name)
            {
                model = l;

                break;
            }
        }

        return model?.Description ?? throw new ArgumentException("Language is not supported");
    }

    public static string ConvertCodeToSymbol(string code)
    {
        var model = (from l in ListSupportedLanguages()
                     where l.Name == code
                     select l).FirstOrDefault();

        return model?.Symbol ?? throw new ArgumentException("Language is not supported");
    }

    public static CultureModel English()
    {
        return new CultureModel("7c349568-5b24-4c68-996a-de9c64ab73e2", LanguageCodes.EnglishCanada, "English - Anglais", "EN");
    }

    public static CultureModel French()
    {
        return new CultureModel("fd6cdf3a-6da4-424d-b69a-b8ba5e38efb3", LanguageCodes.FrenchCanada, "Francais - French", "FR");
    }

    public static List<CultureModel> ListSupportedLanguages()
    {
        var list = new List<CultureModel>
        {
            English(),
            French()
        };

        return list;
    }
}