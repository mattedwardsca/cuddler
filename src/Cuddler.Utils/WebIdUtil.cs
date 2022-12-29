using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace Cuddler.Utils;

public static class WebIdUtil
{
    public static string GetGuidId(string? value)
    {
        return ConvertToMd5HashGuid(value)
            .ToString();
    }

    public static string GetGuidId<T>(Expression<Func<T, object>> selector, string contextId)
    {
        var key = GetMemberInfo(selector)
                  .Member.Name;

        return GetGuidId(key + contextId);
    }

    public static string GetWebId()
    {
        var value = Guid.NewGuid()
                        .ToString("N");

        return GetWebId(value);
    }

    public static string GetWebId(string? value)
    {
        var guid = ConvertToMd5HashGuid(value);

        return "id_" + guid.ToString("N");
    }

    private static Guid ConvertToMd5HashGuid(string? value)
    {
        // convert null to empty string - null can not be hashed
        value ??= string.Empty;

        // get the byte representation
        var bytes = Encoding.Default.GetBytes(value);

        // create the md5 hash
        var md5Hasher = MD5.Create();
        var data = md5Hasher.ComputeHash(bytes);

        // convert the hash to a Guid
        return new Guid(data);
    }

    private static MemberExpression GetMemberInfo<TObject>(Expression<Func<TObject, object>> method)
    {
        if (method is not LambdaExpression lambda)
        {
            throw new ArgumentNullException(nameof(method));
        }

        MemberExpression? memberExpr = null;

        if (lambda.Body.NodeType == ExpressionType.Convert)
        {
            memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
        }
        else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            memberExpr = lambda.Body as MemberExpression;
        }

        if (memberExpr == null)
        {
            throw new InvalidOperationException(nameof(method));
        }

        return memberExpr;
    }
}