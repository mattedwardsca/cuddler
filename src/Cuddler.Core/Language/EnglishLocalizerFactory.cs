using Microsoft.Extensions.Localization;

namespace Cuddler.Core.Language;

internal class EnglishLocalizerFactory : IStringLocalizerFactory
{
    private EnglishLocalizer? _databaseLocalizer;

    public IStringLocalizer Create(Type resourceSource)
    {
        return _databaseLocalizer ??= new EnglishLocalizer();
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return _databaseLocalizer ??= new EnglishLocalizer();
    }
}