using Cuddler.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cuddler.Data.Context;

public interface IRepository
{
    DatabaseFacade Database { get; }

    IModel Model { get; }

    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

    TEntity Get<TEntity>(string id) where TEntity : class, IData;

    EntityEntry<TEntity> Update<TEntity>(string id, IDictionary<string, object?> dictionary) where TEntity : class, IData;

    TEntity Archive<TEntity>(string id) where TEntity : class, IData;

    void Archive<TEntity>(TEntity entity) where TEntity : class, IData;

    IQueryable<IData>? DbSet(string entityType);

    IQueryable<IData> DbSet(Type type);

    DbSet<TEntity> DbSet<TEntity>() where TEntity : class;

    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

    //int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    int SaveChanges();

    EntityEntry Entry(object entity);
}