using Cuddler.Data.Context;
using Cuddler.Data.Entities;
using Cuddler.Data.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cuddler.Data.Repository;

public abstract class BaseRepository : IdentityDbContext<AccountEntity>, IRepository, ITranslationRepository, IBaseRepository
{
    protected BaseRepository(DbContextOptions options) : base(options)
    {
    }

    public DbSet<MessageEntity> Messages { get; set; } = null!;

    public DbSet<MessageUserEntity> MessageUsers { get; set; } = null!;

    public DbSet<AccountRequestEntity> AccountRequests { get; set; } = null!;

    public DbSet<AccountEntity> Accounts { get; set; } = null!;

    public DbSet<AddressEntity> Addresses { get; set; } = null!;

    public DbSet<DocumentEntity> Documents { get; set; } = null!;

    public DbSet<EmailEntity> Emails { get; set; } = null!;

    public DbSet<EmailTemplateEntity> EmailTemplates { get; set; } = null!;

    public DbSet<FieldEntity> Fields { get; set; } = null!;

    public DbSet<OptionEntity> Options { get; set; } = null!;

    public DbSet<OrderEntity> Orders { get; set; } = null!;

    public DbSet<OrganizationEntity> Organizations { get; set; } = null!;

    public DbSet<ProductEntity> Products { get; set; } = null!;

    public DbSet<ProfileEntity> Profiles { get; set; } = null!;

    public DbSet<ProjectEntity> Projects { get; set; } = null!;

    public DbSet<ScheduleEntity> Schedules { get; set; } = null!;

    public DbSet<SupplierEntity> Suppliers { get; set; } = null!;

    public DbSet<TransactionCategoryEntity> TransactionCategories { get; set; } = null!;

    public DbSet<TransactionEntity> Transactions { get; set; } = null!;

    public TEntity Archive<TEntity>(string id) where TEntity : class, IData
    {
        var entity = Get<TEntity>(id);
        entity.DateArchived = DateTime.UtcNow;

        return entity;
    }

    public void Archive<TEntity>(TEntity entity) where TEntity : class, IData
    {
        entity.DateArchived ??= DateTime.UtcNow;
    }

    public IQueryable<IData> DbSet(Type type)
    {
        var name = type.Name.Replace("Entity", string.Empty);
        name = PluralizeUtil.Pluralize(name);

        return DbSet(name);
    }

    public IQueryable<IData> DbSet(string name)
    {
        var repository = GetType();
        var propertyInfo = repository.GetProperty(name);
        var dbSet = propertyInfo?.GetValue(this, null);

        if (dbSet == null)
        {
            throw new InvalidOperationException("Invalid table: " + name);
        }

        return (IQueryable<IData>)dbSet;
    }

    public DbSet<TEntity> DbSet<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public TEntity Get<TEntity>(string id) where TEntity : class, IData
    {
        return DbSet<TEntity>()
            .Single(w => w.Id == id);
    }

    public EntityEntry<TEntity> Update<TEntity>(string id, IDictionary<string, object?> dictionary) where TEntity : class, IData
    {
        throw new NotImplementedException();
    }

    public DbSet<CultureEntity> Cultures { get; set; } = null!;

    public DbSet<ResourceEntity> Resources { get; set; } = null!;

    public string GetUserLanguage(string accountId)
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AccountEntity>()
               .Navigation(n => n.Profile)
               .AutoInclude();

        builder.Entity<ProfileEntity>()
               .Navigation(n => n.Organization)
               .AutoInclude();

        builder.Entity<MessageEntity>()
               .Property(b => b.ReceipientIds)
               .HasConversion(v => string.Join(',', v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
               .Metadata.SetValueComparer(SharedValueComparers.StringArray());

        //builder.Entity<ProductPackageEntity>()
        //       .Property(b => b.ProductIds)
        //       .HasConversion(v => string.Join(',', v!), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
        //       .Metadata.SetValueComparer(SharedValueComparers.StringArray());

        builder.Entity<ProductEntity>()
               .Property(b => b.Specs)
               .HasConversion(v => string.Join(',', v!), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
               .Metadata.SetValueComparer(SharedValueComparers.StringArray());

        builder.Entity<ProductEntity>()
               .Property(b => b.OptionalAddons)
               .HasConversion(v => string.Join(',', v!), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
               .Metadata.SetValueComparer(SharedValueComparers.StringArray());

        builder.Entity<ProductEntity>()
               .Property(b => b.RequiredAddons)
               .HasConversion(v => string.Join(',', v!), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
               .Metadata.SetValueComparer(SharedValueComparers.StringArray());

        builder.Entity<OrganizationEntity>()
               .Property(b => b.BillingContacts)
               .HasConversion(v => string.Join(',', v!), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
               .Metadata.SetValueComparer(SharedValueComparers.StringArray());

        base.OnModelCreating(builder);
    }
}