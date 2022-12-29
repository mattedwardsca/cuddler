using Microsoft.Extensions.Localization;

namespace Cuddler.Core.Language;

internal class EnglishLocalizer<T> : EnglishLocalizer, IStringLocalizer<T>
{
    protected EnglishLocalizer()
    {
    }
}

internal class EnglishLocalizer : IStringLocalizer<L10N>
{
    public LocalizedString this[string key] => new(key, key);

    public LocalizedString this[string key, params object[] arguments]
    {
        get
        {
            var value = string.Format(key, arguments);

            return new LocalizedString(key, value);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
    {
        return new List<LocalizedString>();
    }
}