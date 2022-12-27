﻿using System.Text;

namespace Cuddler.Core.Services.Images;

public static class Base64EncodeUtil
{
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);

        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        return Convert.ToBase64String(plainTextBytes);
    }
}