using Cuddler.Data;
using Cuddler.Data.Models;
using Cuddler.Shared.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Api.Controllers;

public abstract class BaseController : Controller
{
    protected BaseController(IRepository repository)
    {
        Repository = repository;
    }

    protected IRepository Repository { get; }

    protected async Task<ActionResult> ArchiveEntity<TEntity>(string id, Func<TEntity, Task<bool>>? boolTask = null) where TEntity : class, IData
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(id));
        }

        TEntity entity;
        try
        {
            entity = Repository.DbSet<TEntity>()
                               .Single(w => w.Id == id);
        }
        catch (InvalidOperationException)
        {
            throw new FileNotFoundException($"No {typeof(TEntity).Name} exists with Id \"{id}\"");
        }

        var dateArchived = DateTime.UtcNow.ToLocalTime();

        entity.DateUpdated = dateArchived;
        entity.DateArchived = dateArchived;

        if (boolTask != null)
        {
            await boolTask.Invoke(entity);
        }

        Repository.SaveChanges();
        ModelState.Clear();

        await Task.CompletedTask;

        return Json(entity);
    }

    protected async Task<ActionResult> CreateEntity<TEntity>(TEntity entity, Func<TEntity, Task<bool>>? boolTask = null) where TEntity : class, IData
    {
        entity.Id = Guid.NewGuid()
                        .ToString();
        entity.DateCreated = DateTime.UtcNow.ToLocalTime();
        entity.DateUpdated = DateTime.UtcNow.ToLocalTime();
        Repository.Add(entity);

        var validationErrors = ErrorModelUtil.GetModelValidationErrors(entity);
        if (validationErrors.Any())
        {
            return Errors(validationErrors);
        }

        if (boolTask != null)
        {
            await boolTask.Invoke(entity);
        }

        Repository.SaveChanges();
        ModelState.Clear();

        return Json(entity);
    }

    protected ActionResult Errors(IDictionary<string, string> validationErrors)
    {
        foreach (var (key, value) in validationErrors)
        {
            ModelState.AddModelError(key, value);
        }

        Response.StatusCode = 400;

        return Json(null);
    }

    protected IQueryable<TEntity> FilterByDate<TEntity>(IQueryable<TEntity> queryable) where TEntity : class, IData
    {
        var startDate = GetStartDate(Request);
        var endDate = GetEndDate(Request);
        if (startDate != null)
        {
            queryable = queryable.Where(w => w.DateUpdated >= startDate);
        }

        if (endDate != null)
        {
            queryable = queryable.Where(w => w.DateUpdated <= endDate);
        }

        queryable = queryable.OrderByDescending(o => o.DateArchived);

        return queryable;
    }

    protected async Task<ActionResult> RestoreEntity<TEntity>(string id, Func<TEntity, Task<bool>>? boolTask = null) where TEntity : class, IData
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(id));
        }

        TEntity entity;
        try
        {
            entity = Repository.DbSet<TEntity>()
                               .Single(w => w.Id == id);
        }
        catch (InvalidOperationException)
        {
            throw new FileNotFoundException($"No {typeof(TEntity).Name} exists with Id \"{id}\"");
        }

        var dateTime = DateTime.UtcNow.ToLocalTime();

        entity.DateUpdated = dateTime;
        entity.DateArchived = null;

        if (boolTask != null)
        {
            await boolTask.Invoke(entity);
        }

        Repository.SaveChanges();
        ModelState.Clear();

        return Json(entity);
    }

    protected async Task<ActionResult> UpdateEntity<TEntity>(string id, Func<TEntity, Task<bool>>? boolTask = null) where TEntity : class, IData
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(id));
        }

        TEntity entity;
        try
        {
            entity = Repository.DbSet<TEntity>()
                               .Single(w => w.Id == id);
        }
        catch (InvalidOperationException)
        {
            throw new FileNotFoundException($"No {typeof(TEntity).Name} exists with Id \"{id}\"");
        }

        var form = await HttpContext.Request.ReadFormAsync();

        if (form.ContainsKey("cb_hint"))
        {
            form.TryGetValue("cb_hint", out var hints);

            foreach (var hint in hints)
            {
                if (!form.ContainsKey(hint!))
                {
                    UpdateModelUtil.UpdateModelValue(entity, hint!, false);
                }
            }
        }

        UpdateModelUtil.UpdateModelValues(entity, form);

        entity.DateUpdated = DateTime.UtcNow.ToLocalTime();
        var validationErrors = ErrorModelUtil.GetModelValidationErrors(entity);
        if (validationErrors.Any())
        {
            return Errors(validationErrors);
        }

        if (boolTask != null)
        {
            await boolTask.Invoke(entity);
        }

        Repository.SaveChanges();
        ModelState.Clear();

        return Json(entity);
    }

    private static DateTime? GetEndDate(HttpRequest httpRequest)
    {
        return RequestEndDate(httpRequest.HttpContext.Request, false);
    }

    private static DateTime? GetStartDate(HttpRequest httpRequest)
    {
        return RequestStartDate(httpRequest, false);
    }

    private static DateTime? RequestEndDate(HttpRequest request, bool required)
    {
        if (!DateTime.TryParse(request.Query["end"]
                                      .FirstOrDefault(), out var enddate))
        {
            if (required)
            {
                enddate = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                return null;
            }
        }

        return new DateTime(enddate.Ticks - 1).AddDays(1);
    }

    private static DateTime? RequestStartDate(HttpRequest request, bool required)
    {
        if (!DateTime.TryParse(request.Query["start"]
                                      .FirstOrDefault(), out var startdate))
        {
            if (required)
            {
                startdate = DateTime.UtcNow.ToLocalTime();
            }
            else
            {
                return null;
            }
        }

        return startdate.Date;
    }
}