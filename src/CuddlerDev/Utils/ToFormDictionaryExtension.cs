using Microsoft.AspNetCore.Http;

namespace CuddlerDev.Utils;

public static class ToFormDictionaryExtension
{
    public static Dictionary<string, object?> ToFormDictionary(this IFormCollection formCollection)
    {
        var dictionary = new Dictionary<string, object?>();
        foreach (var key in formCollection.Keys)
        {
            if (key.StartsWith("_"))
            {
                continue;
            }

            if (key == "cb_hint")
            {
                var propertyKey = formCollection[key]
                    .ToString();
                var formhasSameKey = formCollection[propertyKey];
                if (formhasSameKey.Count == 0)
                {
                    dictionary.Add(propertyKey, false);
                }

                continue;

            }

            var value = "";
            if (formCollection[key]
                    .Count
                > 0)
            {
                if (formCollection[key]
                        .Count
                    == 1)
                {
                    value = formCollection[key][0];
                }
                else
                {
                    value = formCollection[key];
                }
            }

            dictionary.Add(key, value);
        }

        return dictionary;
    }
}