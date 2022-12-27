using System.Linq.Expressions;

namespace Cuddler.Core.Services.Settings;

public interface ISettingsService
{
    void Delete(string key);

    string? Get(string key);

    TType? Get<TType>(Expression<Func<GlobalSettings, TType?>> property);

    TType? GetSettings<TType>(string key) where TType : class;

    TType GetSettingsObject<TType>();

    TType GetSettingsObject<TType>(TType obj);

    bool IsEnabled(string? contextId, string key);

    bool IsEqual(string key, string? compareValue);

    bool IsTrue(Expression<Func<GlobalSettings, object?>> property);

    bool IsTrueHash(string key);

    void Put(string key, string? value);

    void PutSettings<T>(string key, T obj) where T : class;

    void SetObject<TType>(TType obj) where TType : class;
}