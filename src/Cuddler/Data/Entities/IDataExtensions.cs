namespace Cuddler.Data.Entities;

public static class IDataExtensions
{
    public static TData Init<TData>(this TData entity, string? id = null) where TData : IData
    {
        entity.Id = id
                    ?? Guid.NewGuid()
                           .ToString();

        var dateCreated = DateTime.UtcNow.ToLocalTime();
        entity.DateCreated = dateCreated;
        entity.DateUpdated = dateCreated;

        return entity;
    }
}