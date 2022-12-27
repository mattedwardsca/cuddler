using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Core.Services.Settings;

public class WebsiteSettings
{
    public string AboutWebsite { get; set; } = "Order Management System";

    [HiddenInput]
    public bool AllowUnauthorizedHomepage { get; set; }

    public string CopyrightHolder { get; set; } = "Cocoon CS Inc.";

    public string? DefaultLanguage { get; set; } = "EN";

    public string SupportEmail { get; set; } = "support@cocooncs.com";

    public string? TermsPath { get; set; }

    public string WebsiteFaviconPath { get; set; } = "/static/images/favicon.png";

    public string WebsiteLogoPath { get; set; } = "/static/images/logo.png";

    public string? WebsiteBillingCity { get; set; } = "Calgary";

    public string? WebsiteBillingEmail { get; set; } = "billing@orderra.com";

    public string? WebsiteBillingPhone { get; set; } = "5878773008";

    public string? WebsiteBillingPostalCode { get; set; } = "TOM 0J0";

    public string? WebsiteBillingProvince { get; set; } = "AB";

    public string? WebsiteBillingStreet1 { get; set; } = "Box 1408";

    public string? WebsiteBillingStreet2 { get; set; } //= "92 Paramount Cr";

    public string? WebsiteName { get; set; } = "Cocoon";

    public string? WebsiteShippingCity { get; set; } = "Calgary";

    public double WebsiteShippingLatitude { get; set; }

    public double WebsiteShippingLongitude { get; set; }

    public string? WebsiteShippingPostalCode { get; set; } = "TOM 0J0";

    public string? WebsiteShippingProvince { get; set; } = "AB";

    public string? WebsiteShippingStreet1 { get; set; } = "Box 1408";

    public string? WebsiteShippingStreet2 { get; set; }

    public void Reset(WebsiteSettings? result)
    {
        if (result != null)
        {
            WebsiteLogoPath = result.WebsiteLogoPath;
            WebsiteName = result.WebsiteName;
        }
    }

    public string ToBillingAddress(bool asHtml = true)
    {
        var firstItem = true;
        var sb = new StringBuilder();
        if (!string.IsNullOrEmpty(WebsiteBillingStreet1))
        {
            sb.Append(WebsiteBillingStreet1);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(WebsiteBillingStreet2))
        {
            if (!string.IsNullOrEmpty(WebsiteBillingStreet1))
            {
                sb.Append(", ");
            }

            sb.Append(WebsiteBillingStreet2);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(WebsiteBillingCity) || !string.IsNullOrEmpty(WebsiteBillingProvince))
        {
            if (!firstItem)
            {
                sb.Append(asHtml
                    ? "<br/>"
                    : "\n");
            }

            sb.Append(WebsiteBillingCity);
            if (!string.IsNullOrEmpty(WebsiteBillingCity) && !string.IsNullOrEmpty(WebsiteBillingProvince))
            {
                sb.Append(", ");
            }

            sb.Append(WebsiteBillingProvince);

            firstItem = false;
        }

        if (!string.IsNullOrEmpty(WebsiteBillingPostalCode))
        {
            if (!firstItem)
            {
                sb.Append(", ");
            }

            sb.Append(WebsiteBillingPostalCode);
        }

        return sb.ToString();
    }

    public string ToShippingAddress(bool asHtml = true)
    {
        var firstItem = true;
        var sb = new StringBuilder();
        if (!string.IsNullOrEmpty(WebsiteShippingStreet1))
        {
            sb.Append(WebsiteShippingStreet1);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(WebsiteShippingStreet2))
        {
            if (!string.IsNullOrEmpty(WebsiteShippingStreet1))
            {
                sb.Append(", ");
            }

            sb.Append(WebsiteShippingStreet2);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(WebsiteShippingCity) || !string.IsNullOrEmpty(WebsiteShippingProvince))
        {
            if (!firstItem)
            {
                sb.Append(asHtml
                    ? "<br/>"
                    : "\n");
            }

            sb.Append(WebsiteShippingCity);
            if (!string.IsNullOrEmpty(WebsiteShippingCity) && !string.IsNullOrEmpty(WebsiteShippingProvince))
            {
                sb.Append(", ");
            }

            sb.Append(WebsiteShippingProvince);

            firstItem = false;
        }

        if (!string.IsNullOrEmpty(WebsiteShippingPostalCode))
        {
            if (!firstItem)
            {
                sb.Append(", ");
            }

            sb.Append(WebsiteShippingPostalCode);
        }

        return sb.ToString();
    }
}