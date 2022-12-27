using Cuddler.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cuddler.Web.Language;

public interface ITranslationRepository
{
    DbSet<CultureEntity> Cultures { get; set; }

    DbSet<ResourceEntity> Resources { get; set; }

    string? GetUserLanguage(string accountId);
}