using Cuddler.Core.Attributes;
using Cuddler.Core.Context;
using Cuddler.Core.Models;
using Cuddler.Core.Utils;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Core.Controllers;

public abstract class TreeEntityApiBaseController<TEntity> : BaseController where TEntity : class, IData, IHasParent
{
    protected TreeEntityApiBaseController(IRepository repository) : base(repository)
    {
    }

    [HttpPost(nameof(Active))]
    public async Task<ActionResult> Active(string q, [DataSourceRequest] DataSourceRequest request)
    {
        request.Filters = FilterDescriptorFactory.Create(q);

        var queryable = Repository.DbSet<TEntity>()
                                  .Where(w => w.DateArchived == null);

        var data = await OnListActive(queryable);

        return Json(data.ToDataSourceResult(request));
        // return Json(data.ToTreeDataSourceResult(request, e => e.Id, e => e.ParentId, e => e));
    }

    //public JsonResult All([DataSourceRequest] DataSourceRequest request)
    //{
    //    var result = Repository.DbSet<TEntity>()
    //                           .ToTreeDataSourceResult(request, e => e.Id, e => e.ParentId, e => e);

    //    return Json(result);
    //}

    [HandleErrors]
    [WrapOutput]
    [HttpPost(nameof(Archive))]
    public async Task<ActionResult> Archive([DataSourceRequest] DataSourceRequest request, string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(id));
        }

        var entity = Repository.DbSet<TEntity>()
                               .Single(w => w.Id == id);
        entity.DateArchived = DateTime.UtcNow.ToLocalTime();
        await OnArchived(entity);
        await Repository.SaveChangesAsync();
        var entities = new[]
        {
            entity
        };

        return Json(entities.ToTreeDataSourceResult(request, ModelState));
    }

    [HttpPost(nameof(Archived))]
    public async Task<IActionResult> Archived([DataSourceRequest] DataSourceRequest request)
    {
        var queryable = Repository.DbSet<TEntity>()
                                  .Where(w => w.DateArchived != null);
        var entities = await OnListArchived(queryable);

        return Json(entities.ToTreeDataSourceResult(request, e => e.Id, e => e.ParentId, e => e));
    }

    [HandleErrors]
    [WrapOutput]
    [ValidateModel]
    [HttpPost(nameof(Create))]
    public async Task<ActionResult> Create([DataSourceRequest] DataSourceRequest request, TEntity entity)
    {
        if (string.IsNullOrEmpty(entity.Id))
        {
            entity.Id = Guid.NewGuid()
                            .ToString();
        }

        entity.DateCreated = DateTime.UtcNow.ToLocalTime();
        entity.DateUpdated = DateTime.UtcNow.ToLocalTime();
        Repository.Add(entity);
        await OnCreate(entity);

        var validationErrors = ErrorModelUtil.GetValidationErrors(entity);
        if (validationErrors.Any())
        {
            return Errors(validationErrors);
        }

        await Repository.SaveChangesAsync();
        var entities = new[]
        {
            entity
        };

        return Json(entities.ToTreeDataSourceResult(request, ModelState));
    }

    [HandleErrors]
    [WrapOutput]
    [HttpPost(nameof(Destroy))]
    public async Task<ActionResult> Destroy([DataSourceRequest] DataSourceRequest request, string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(id));
        }

        var entity = Repository.DbSet<TEntity>()
                               .Single(w => w.Id == id);
        await OnDestroy(entity);
        await Repository.SaveChangesAsync();
        var entities = new[]
        {
            entity
        };

        return Json(entities.ToTreeDataSourceResult(request, ModelState));
    }

    [HandleErrors]
    [WrapOutput]
    [HttpPost(nameof(Get))]
    public ActionResult Get(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(id));
        }

        var entity = Repository.DbSet<TEntity>()
                               .Single(w => w.Id == id);

        return Json(entity);
    }

    public JsonResult Index([DataSourceRequest] DataSourceRequest request, string? id)
    {
        var result = Repository.DbSet<TEntity>()
                               .ToTreeDataSourceResult(request, e => e.Id, e => e.ParentId, e => !string.IsNullOrEmpty(id)
                                   ? e.ParentId == id
                                   : e.ParentId == null, e => e);

        return Json(result);
    }

    public abstract Task OnArchived(TEntity entity);

    public abstract Task OnCreate(TEntity entity);

    public abstract Task<TEntity> OnDestroy(TEntity entity);

    public abstract Task<IQueryable<TEntity>> OnListActive(IQueryable<TEntity> list);

    public abstract Task<IQueryable<TEntity>> OnListArchived(IQueryable<TEntity> list);

    public abstract Task OnUnarchived(TEntity entity);

    public abstract Task OnUpdated(TEntity entity);

    [HandleErrors]
    [WrapOutput]
    [HttpPost(nameof(Unarchive))]
    public async Task<ActionResult> Unarchive([DataSourceRequest] DataSourceRequest request, string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(id));
        }

        var entity = Repository.DbSet<TEntity>()
                               .Single(w => w.Id == id);
        entity.DateArchived = null;
        await OnUnarchived(entity);
        await Repository.SaveChangesAsync();
        var entities = new[]
        {
            entity
        };

        return Json(entities.ToTreeDataSourceResult(request, ModelState));
    }

    [HandleErrors]
    [WrapOutput]
    [HttpPost(nameof(Update))]
    public async Task<ActionResult> Update([DataSourceRequest] DataSourceRequest request, string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(id));
        }

        var entity = Repository.DbSet<TEntity>()
                               .Single(w => w.Id == id);
        var form = await HttpContext.Request.ReadFormAsync();
        UpdateModelUtil.UpdateModelValues(entity, form);
        entity.DateUpdated = DateTime.UtcNow.ToLocalTime();

        await OnUpdated(entity);

        var validationErrors = ErrorModelUtil.GetValidationErrors(entity);
        if (validationErrors.Any())
        {
            var errorDictionary = new Dictionary<string, string>();
            foreach (var (key, value) in validationErrors)
            {
                errorDictionary.Add(key, value);
            }

            Response.StatusCode = 400;

            return Json(errorDictionary);
        }

        await Repository.SaveChangesAsync();
        var entities = new[]
        {
            entity
        };

        return Json(entities.ToTreeDataSourceResult(request, ModelState));
    }
}