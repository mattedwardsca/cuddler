using Cuddler.Core.Language;
using Cuddler.Data.Context;
using Cuddler.Data.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Cuddler.Configuration.Internal;

internal class DatabaseLocalizerFactory : IStringLocalizerFactory
{
    private readonly IMemoryCache _cache;
    private readonly IRepository _repository;
    private DatabaseLocalizer? _databaseLocalizer;

    public DatabaseLocalizerFactory(IRepository repository, IMemoryCache cache)
    {
        _cache = cache;
        _repository = repository;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        return _databaseLocalizer ??= new DatabaseLocalizer(_repository, _cache);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return _databaseLocalizer ??= new DatabaseLocalizer(_repository, _cache);
    }

    public async Task Init()
    {

        if (!_repository.DbSet<CultureEntity>()
                        .Any())
        {
            var list = LanguageUtil.ListSupportedLanguages();
            foreach (var cultureModel in list)
            {
                _repository.DbSet<CultureEntity>()
                           .AddRange(new CultureEntity
                           {
                               DateArchived = null,
                               DateCreated = DateTime.UtcNow.ToLocalTime(),
                               DateUpdated = DateTime.UtcNow.ToLocalTime(),
                               Description = cultureModel.Description,
                               Id = cultureModel.Id,
                               Name = cultureModel.Name,
                               Symbol = cultureModel.Symbol
                           });
            }

            await _repository.SaveChangesAsync();
        }
    }

    internal DatabaseLocalizer? Get()
    {
        return _databaseLocalizer;
    }
}