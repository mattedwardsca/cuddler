using System.Security.Claims;
using Cuddler.Configuration.Internal;
using Cuddler.Data.Repository;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Configuration.Language;

internal class LanguageService : ILanguageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITranslationRepository _translationRepository;

    public LanguageService(IHttpContextAccessor httpContextAccessor, ITranslationRepository translationRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _translationRepository = translationRepository;
    }

    public string GetUserLanguage()
    {
        string? language = null;

        // Get cart for user that has cart cookie
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)
                                         ?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            language = _translationRepository.GetUserLanguage(userId);
        }

        language ??= _httpContextAccessor.HttpContext?.Session.GetString(LanguageConstants.CookieName);

        return language ?? LanguageCodes.EnglishCanada;
    }
}