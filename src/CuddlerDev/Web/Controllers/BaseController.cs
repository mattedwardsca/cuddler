﻿using System.Globalization;
using System.Text;
using CsvHelper;
using CuddlerDev.Data.Attributes;
using CuddlerDev.Data.Context;
using CuddlerDev.Data.Entities;
using CuddlerDev.Forms.Utils;
using CuddlerDev.Utils;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuddlerDev.Web.Controllers;

public abstract class BaseController : Controller
{
    protected BaseController(IRepository repository)
    {
        Repository = repository;
    }

    protected IRepository Repository { get; }

    [WrapOutput]
    [HttpPost(nameof(Heartbeat))]
    public async Task<IActionResult> Heartbeat()
    {
        var result = await HeartbeatTests();

        return Json(result);
    }

    protected static IEnumerable<TResult> Filter<TSource, TResult>(DataSourceRequest request, IQueryable<TSource> data, string q, Func<TSource, int, TResult> selector)
    {
        var filters = FilterDescriptorFactory.Create(q);

        request.Filters = filters;

        return data.ToDataSourceResult(request)
                   .Data.Cast<TSource>()
                   .Select(selector);
    }

    protected async Task<IActionResult> ActiveEntities<T>(DataSourceRequest request) where T : class, IData
    {
        var queryable = Repository.DbSet<T>()
                                  .Where(w => w.DateArchived == null);

        return Json(await queryable.ToDataSourceResultAsync(request));
    }

    protected async Task<IActionResult> ArchivedEntities<T>(DataSourceRequest request) where T : class, IData
    {
        var queryable = Repository.DbSet<T>()
                                  .Where(w => w.DateArchived != null);

        return Json(await queryable.ToDataSourceResultAsync(request));
    }

    protected async Task<IActionResult> ArchiveEntity<TEntity>(string id, Func<TEntity, Task<bool>>? boolTask = null) where TEntity : class, IData
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

        await Repository.SaveChangesAsync();
        ModelState.Clear();

        await Task.CompletedTask;

        return Json(entity);
    }

    protected async Task<IActionResult> CreateEntity<TEntity>(TEntity entity, Func<TEntity, Task<bool>>? boolTask = null) where TEntity : class, IData
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

        await Repository.SaveChangesAsync();
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

    protected async Task<IActionResult> ExportCsv<T>(string fileDownloadName, IEnumerable<T> records)
    {
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
        var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        csvWriter.WriteHeader<T>();
        csvWriter.WriteRecords(records);
        csvWriter.Flush();

        await Task.CompletedTask;

        return File(memoryStream.ToArray(), "text/csv", $"{fileDownloadName}.csv");
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

    protected abstract Task<bool> HeartbeatTests();

    protected async Task<IActionResult> RestoreEntity<TEntity>(string id, Func<TEntity, Task<bool>>? boolTask = null) where TEntity : class, IData
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

        await Repository.SaveChangesAsync();
        ModelState.Clear();

        return Json(entity);
    }

    protected async Task<IActionResult> UpdateEntity<TEntity>(string id, Func<TEntity, Task<bool>>? boolTask = null) where TEntity : class, IData
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

        await Repository.SaveChangesAsync();
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