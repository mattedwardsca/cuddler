using CuddlerDev.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuddlerDev.Data.Repository;

public interface ITranslationRepository
{
    DbSet<CultureEntity> Cultures { get; set; }

    DbSet<ResourceEntity> Resources { get; set; }

    string? GetUserLanguage(string accountId);
}