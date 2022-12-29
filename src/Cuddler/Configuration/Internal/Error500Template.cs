namespace Cuddler.Configuration.Internal;

internal static class Error500Template
{
    public static string Format(string title, int responseStatusCode, string? remoteIpAddress, string? url, string? errorDetails, string? resolutionInstructions, string? errorMessage)
    {
        var dateTime = DateTime.UtcNow.ToLocalTime();

        var template = "<body cz-shortcut-listen=\"true\"><div id=\"p\" class=\"P\" style=\"display: block;\"><div class=\"K\">"
                       + $"{responseStatusCode}"
                       + "</div><div class=\"O I\">"
                       + $"{title}"
                       + "</div><p class=\"J A L\">"
                       //+ "Error Times: Mon, 15 Mar 2021 08:24:52 GMT"
                       + $"Error Times: {dateTime:ddd, dd MMM yyyy HH:mm:ss} GMT"
                       + "<br><span class=\"F\">"
                       + $"IP: {remoteIpAddress}"
                       + "</span>"
                       + "<br>"
                       + $"URL: {url}"
                       + "<br><span class=\"hide_me\">Please contact our support: <a href=\"mailto:\"></a><br></span>"
                       + "Check: <span class=\"C G\" onclick=\"s(0)\">Details</span></p></div><div id=\"d\" class=\"hide_me P H\" style=\"display: none;\">"
                       + "<div class=\"K\">ERROR</div>"
                       + "<p class=\"O I\">An error occured while trying to retrieve the URL:</p><div class=\"O\"><pre class=\"B G\">"
                       + $"{url}"
                       + "</pre></div><div class=\"M\">"
                       + "<span>The following error was encountered:</span><ul class=\"E\"><li class=\"D G\">"
                       + $"{errorDetails}"
                       + "</li></ul></div><p>"
                       + "<p class=\"M\">The system returned:</p>"
                       + "<p class=\"MY\">"
                       + $"{errorMessage}"
                       + "</p><br>"
                       + $"{resolutionInstructions}"
                       + "</p><a class=\"N C\" href=\"#\" onclick=\"s(1)\">hide details</a></div>"
                       + "<script type=\"text/javascript\">function e(i){return document.getElementById(i);}function d(i,t){e(i).style.display=(t?'block':'none');}function s(e){d('p',e);d('d',!e);}</script>"
                       + "</body>";

        return template;
    }
}