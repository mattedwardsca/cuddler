using System.Globalization;
using Cuddler.Data.Context;
using Cuddler.Data.Entities;
using Cuddler.Web.Language;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Cuddler.Web.Configuration.Internal;

internal class DatabaseLocalizer<T> : DatabaseLocalizer, IStringLocalizer<T>
{
    protected DatabaseLocalizer(IRepository repository, IMemoryCache cache) : base(repository, cache)
    {
    }
}

internal class DatabaseLocalizer : IStringLocalizer<L10N>
{
    private readonly IMemoryCache _cache;
    private readonly IRepository _db;

    public DatabaseLocalizer(IRepository repository, IMemoryCache cache)
    {
        _db = repository;
        _cache = cache;
    }

    public LocalizedString this[string key]
    {
        get
        {
            var value = GetString(key);

            return new LocalizedString(key, value ?? key, value == null);
        }
    }

    public LocalizedString this[string key, params object[] arguments]
    {
        get
        {
            var format = GetString(key);
            var value = string.Format(format ?? key, arguments);

            return new LocalizedString(key, value, format == null);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
    {
        var resources = from r in _db.DbSet<ResourceEntity>()
                        join c in _db.DbSet<CultureEntity>() on r.CultureId equals c.Id
                        where c.Name == CultureInfo.CurrentCulture.Name
                        select new LocalizedString(r.Key, r.Value, true);

        return resources;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public void Set(string key, string translation)
    {
        var cacheKey = GetCacheKey(key);

        SetCacheKey(cacheKey, translation);
    }

    public IStringLocalizer<L10N> WithCulture(CultureInfo culture, IMemoryCache cache)
    {
        CultureInfo.DefaultThreadCurrentCulture = culture;

        return new DatabaseLocalizer(_db, cache);
    }

    private static string GetCacheKey(string key)
    {
        return key + CultureInfo.CurrentCulture.Name;
    }

    private string? GetString(string? key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(key));
        }

        var cacheKey = GetCacheKey(key);

        if (!_cache.TryGetValue(cacheKey, out string? translation))
        {
            translation = (from r in _db.DbSet<ResourceEntity>()
                           join c in _db.DbSet<CultureEntity>() on r.CultureId equals c.Id
                           where c.Name == CultureInfo.CurrentCulture.Name && r.Key == key
                           select r.Value).FirstOrDefault();

            SetCacheKey(cacheKey, translation);
        }

        return translation;
    }

    private void SetCacheKey(string key, string? translation)
    {
        _cache.Set(key, translation, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60)));
    }
}