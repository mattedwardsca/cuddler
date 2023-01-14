using System.Text.Json;
using System.Xml.Serialization;

namespace CuddlerDev.Data.Utils;

public static class SerializationUtil
{
    public static IDictionary<string, object> JsonDeserializeDictionary(string json)
    {
        var deserializeObject = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

        return deserializeObject ?? new Dictionary<string, object>();
    }

    public static object JsonDeserializeObject(Type type, string? json)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (string.IsNullOrEmpty(json))
        {
            return Activator.CreateInstance(type)!;
        }

        return JsonSerializer.Deserialize(json, type) ?? Activator.CreateInstance(type)!;
    }

    public static string JsonSerializeDictionary(Dictionary<string, object?> form)
    {
        var dictionary = new Dictionary<string, string?>();
        foreach (var (key, o) in form)
        {
            var value = "";
            if (o != null)
            {
                value = o.ToString();
            }

            dictionary.Add(key, value);
        }

        return JsonSerializer.Serialize(dictionary);
    }

    public static string JsonSerializeObject(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public static Dictionary<string, string> XmlDeserializeDictionary(string formData)
    {
        return (string.IsNullOrEmpty(formData)
                   ? new Dictionary<string, string>()
                   : JsonSerializer.Deserialize<Dictionary<string, string>>(formData))
               ?? new Dictionary<string, string>();
    }

    public static T XmlDeserializeObject<T>(string? objectToDerialize) where T : class
    {
        if (string.IsNullOrEmpty(objectToDerialize))
        {
            return (Activator.CreateInstance(typeof(T)) as T)!;
        }

        var xmlSerializer = new XmlSerializer(typeof(T));
        var textReader = new StringReader(objectToDerialize);

        return (T)(xmlSerializer.Deserialize(textReader) ?? throw new InvalidOperationException());
    }

    public static object XmlDeserializeObject(Type t, string? objectToDerialize)
    {
        if (string.IsNullOrEmpty(objectToDerialize))
        {
            return Activator.CreateInstance(t) ?? throw new InvalidOperationException("f100be91-0804-4c6c-9261-8e759d1b1eea");
        }

        var xmlSerializer = new XmlSerializer(t);
        var textReader = new StringReader(objectToDerialize);

        return xmlSerializer.Deserialize(textReader) ?? throw new InvalidOperationException("b1ba2517-b888-47fd-93c1-4621e32ef28c");
    }

    public static string XmlSerializeObject(object objectToSerialize)
    {
        var xmlSerializer = new XmlSerializer(objectToSerialize.GetType());
        var textWriter = new StringWriter();
        xmlSerializer.Serialize(textWriter, objectToSerialize);

        return textWriter.ToString();
    }
}