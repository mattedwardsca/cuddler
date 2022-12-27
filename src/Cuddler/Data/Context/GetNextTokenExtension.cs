using Cuddler.Data.Attributes;
using Cuddler.Data.Entities;

namespace Cuddler.Data.Context;

public static class GetNextTokenExtension
{
    public static string GetNextToken(this IRepository repository, Type t)
    {
        var incrementBy = 1;

        var prefix = GetPrefix(t);

        while (true)
        {
            var count = repository.DbSet(t)
                                  .Count()
                        + incrementBy;
            const string fmt = "0000";
            var str = prefix + "-" + count.ToString(fmt);

            var existsAlready = repository.DbSet(t)
                                          .Cast<IHasToken>()
                                          .Any(w => w.Token == str);
            if (existsAlready)
            {
                incrementBy += 1;

                continue;
            }

            return str;
        }
    }

    private static string GetPrefix(Type type)
    {
        var props = type.GetProperties();
        var tokenProperty = props.First(w => w.Name == nameof(IHasToken.Token));
        var attrs = tokenProperty.GetCustomAttributes(true)
                                 .FirstOrDefault(w => w.GetType() == typeof(TokenNameAttribute));
        if (attrs is TokenNameAttribute tokenNameAttr)
        {
            return tokenNameAttr.Name;
        }

        return type.Name[..3];
    }

    public static string GetNextToken<T>(this IRepository repository) where T : class, IHasToken
    {
        return repository.GetNextToken<T>(typeof(T).Name[..3]
                                                   .ToUpper());
    }

    public static string GetNextToken<T>(this IRepository repository, string prefix) where T : class, IHasToken
    {
        if (string.IsNullOrEmpty(prefix))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(prefix));
        }

        var incrementBy = 1;

        while (true)
        {
            var count = repository.DbSet<T>()
                                  .Count()
                        + incrementBy;
            const string fmt = "0000";
            var str = prefix + "-" + count.ToString(fmt);

            var existsAlready = repository.DbSet<T>()
                                          .Any(w => w.Token == str);
            if (existsAlready)
            {
                incrementBy += 1;

                continue;
            }

            return str;
        }
    }
}