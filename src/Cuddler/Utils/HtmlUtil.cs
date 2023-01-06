using System.Text;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Utils;

public static class HtmlUtil
{
    public static string CleanUrl(string untrustedHtml)
    {
        if (untrustedHtml.Contains("//"))
        {
            untrustedHtml = untrustedHtml.Replace("//", "/");

            return CleanUrl(untrustedHtml);
        }

        return untrustedHtml;
    }

    // ReSharper disable once UnusedParameter.Global
    public static string IntArray(this IHtmlHelper _, int start, int end)
    {
        var sb = new StringBuilder();
        sb.Append("[");
        for (var i = start; i <= end; i++)
        {
            sb.Append(i);
            sb.Append(",");
        }

        var str = sb.ToString();
        str = str.Substring(0, str.Length - 1);
        str = str + "]";

        return str;

        // [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24]
    }

    public static string SafeHtml(string untrustedHtml)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(untrustedHtml);
        doc.DocumentNode.Descendants()
           .Where(n => n.Name == "script" || n.Name == "style")
           .ToList()
           .ForEach(n => n.Remove());

        RemoveStyles(doc);

        RemoveClasses(doc);

        RemoveButtons(doc);

        RemoveLinks(doc);

        return doc.DocumentNode.OuterHtml;
    }

    // ReSharper disable once UnusedParameter.Global
    public static IHtmlContent ToJavascriptArray(this IHtmlHelper _, string[]? arr)
    {
        if (arr == null)
        {
            return new HtmlString("[]");
        }

        if (arr.Length == 1)
        {
            return new HtmlString($"[\"{arr[0]}\"]");
        }

        var sb = new StringBuilder();
        sb.Append("[");
        sb.Append($"\"{arr[0]}\"");

        for (var i = 1; i < arr.Length; i++)
        {
            sb.Append($",\"{arr[i]}\"");
        }

        sb.Append("]");
        var str = sb.ToString();

        return new HtmlString(str);
    }

    private static void RemoveButtons(HtmlDocument doc)
    {
        var buttons = doc.DocumentNode.SelectNodes("//button");
        if (buttons != null)
        {
            foreach (var node in buttons)
            {
                node.Name = "input";
                node.Attributes["type"]
                    ?.Remove();
                node.Attributes.Add("type", "button");
                node.Attributes.Add("value", node.InnerHtml);
                node.InnerHtml = string.Empty;
            }
        }
    }

    private static void RemoveClasses(HtmlDocument doc)
    {
        var classAttribute = doc.DocumentNode.SelectNodes("//@class");
        if (classAttribute != null)
        {
            foreach (var element in classAttribute)
            {
                element.Attributes["class"]
                       .Remove();
            }
        }
    }

    private static void RemoveLinks(HtmlDocument doc)
    {
        var links = doc.DocumentNode.SelectNodes("//a");
        if (links != null)
        {
            foreach (var node in links)
            {
                node.Attributes["target"]
                    ?.Remove();
                node.Attributes.Add("target", "_blank");
            }
        }
    }

    private static void RemoveStyles(HtmlDocument doc)
    {
        var styleAttribute = doc.DocumentNode.SelectNodes("//@style");
        if (styleAttribute != null)
        {
            foreach (var element in styleAttribute)
            {
                element.Attributes["style"]
                       .Remove();
            }
        }
    }
}