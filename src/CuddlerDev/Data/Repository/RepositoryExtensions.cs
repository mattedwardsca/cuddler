using CuddlerDev.Data.Context;
using CuddlerDev.Data.Entities;

namespace CuddlerDev.Data.Repository;

public static class RepositoryExtensions
{
    public static string GetNextUserName(this IRepository repository, string prefix = "ACC-")
    {
        if (string.IsNullOrEmpty(prefix) || prefix == "-")
        {
            prefix = "ACC-";
        }

        if (!prefix.EndsWith('-'))
        {
            prefix += "-";
        }

        var previous = repository.DbSet<AccountEntity>()
                                 .Where(w => w.UserName.Contains(prefix) && w.UserName.Length < 12)
                                 .OrderBy(o => o.UserName)
                                 .LastOrDefault();

        if (previous?.UserName != null && !previous.UserName.Contains('@'))
        {
            var start = prefix.Length;

            if (!string.IsNullOrEmpty(previous.UserName) && previous.UserName.Length > start + 1)
            {
                var nextNumber = int.Parse(previous.UserName.Split('-')
                                                   .Last())
                                 + 1;
                return $"{prefix}" + nextNumber.ToString("D6");
            }
        }

        return prefix + "000001";
    }
}