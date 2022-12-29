using Cuddler.Data.Context;
using Cuddler.Data.Entities;
using Cuddler.Forms.Utils;

namespace Cuddler.Web.Blocks;

public interface IDynamicService : IService
{
    Task<DynamicActionResult<TEntity>> Update<TEntity>(IDictionary<string, object?> formDictionary) where TEntity : class, IData;
    Task<DynamicActionResult<TEntity>> Archive<TEntity>(IDictionary<string, object?> formDictionary) where TEntity : class, IData;
    Task<DynamicActionResult<TEntity>> Create<TEntity>(IDictionary<string, object?> formDictionary) where TEntity : class, IData;
}

public class DynamicService : IDynamicService
{
    private readonly IRepository _repository;

    public DynamicService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<DynamicActionResult<TEntity>> Create<TEntity>(IDictionary<string, object?> formDictionary) where TEntity : class, IData
    {
        var id = formDictionary.GetValue(nameof(IData.Id));
        TEntity? get;
        if (id != null)
        {
            get = _repository.DbSet<TEntity>()
                             .Find(id);
            if (get != null)
            {
                return DynamicActionResult<TEntity>.Error400(nameof(IData.Id), $"{typeof(TEntity).Name} with {nameof(IData.Id)} '{id}' already exists.");
            }
        }

        get = Activator.CreateInstance<TEntity>();
        get.Init();

        UpdateModelUtil.UpdateModelValues(get, formDictionary);

        var errors = ValidateModelUtil.GetModelValidationErrors(get);
        if (errors.Any())
        {
            return new DynamicActionResult<TEntity>(errors);
        }

        await Task.CompletedTask;

        _repository.Add(get);
        await _repository.SaveChangesAsync();

        return new DynamicActionResult<TEntity>(get);
    }

    public async Task<DynamicActionResult<TEntity>> Archive<TEntity>(IDictionary<string, object?> formDictionary) where TEntity : class, IData
    {
        var id = formDictionary.GetValue(nameof(IData.Id)) ?? throw new ArgumentNullException(nameof(IData.Id));
        var get = _repository.DbSet<TEntity>()
                             .Find(id);

        if (get == null)
        {
            return DynamicActionResult<TEntity>.Error404(nameof(IData.Id), id);
        }

        get.DateArchived = DateTime.UtcNow.ToLocalTime();

        await _repository.SaveChangesAsync();

        return new DynamicActionResult<TEntity>(get);
    }

    public async Task<DynamicActionResult<TEntity>> Update<TEntity>(IDictionary<string, object?> formDictionary) where TEntity : class, IData
    {
        var id = formDictionary.GetValue(nameof(IData.Id)) ?? throw new ArgumentNullException(nameof(IData.Id));
        var get = _repository.DbSet<TEntity>()
                             .Find(id);

        if (get == null)
        {
            return DynamicActionResult<TEntity>.Error404(nameof(IData.Id), id);
        }

        UpdateModelUtil.UpdateModelValues(get, formDictionary);

        var errors = ValidateModelUtil.GetModelValidationErrors(get);
        if (errors.Any())
        {
            return new DynamicActionResult<TEntity>(errors);
        }

        await Task.CompletedTask;

        await _repository.SaveChangesAsync();

        return new DynamicActionResult<TEntity>(get);
    }
}