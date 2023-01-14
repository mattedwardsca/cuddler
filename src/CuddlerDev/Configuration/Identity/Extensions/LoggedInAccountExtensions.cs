using System.Security.Claims;
using CuddlerDev.Configuration.Identity.Exceptions;
using CuddlerDev.Data.Context;
using CuddlerDev.Data.Entities;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;

namespace CuddlerDev.Configuration.Identity.Extensions;

public static class LoggedInAccountExtensions
{
    public const string SessionAccountToken = "71e8561a-47d4-43ff-838d-4770bac6d779";

    public static bool IsUserAdmin(this HttpContext httpContext)
    {
        return GetLoggedInAccount(httpContext)
            .IsAdmin;
    }

    public static AccountEntity GetLoggedInAccount(this HttpContext httpContext)
    {
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ??  throw new InvalidClaimException("User is not logged in. Error: 77f90ffa-6ad7-4172-af7a-ce30b3b682dc");

        var getAccountEntityById = httpContext.Items[SessionAccountToken] ?? (httpContext.Items[SessionAccountToken] = Get(httpContext, userId));

        if (!string.IsNullOrEmpty(userId) && getAccountEntityById == null)
        {
            throw new InvalidClaimException("User is not logged in. Error: 1d47a66e-6567-4848-8fb4-57793dd71d28");
        }

        return (AccountEntity)getAccountEntityById!;
    }

    public static string GetLoggedInAccountId(this HttpContext httpContext)
    {
        return GetLoggedInAccount(httpContext)
            .Id;
    }

    public static bool IsUserLoggedIn(this HttpContext httpContext)
    {
        return !string.IsNullOrEmpty(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)
                                                ?.Value);
    }

    private static AccountEntity? Get(HttpContext httpContext, string userId)
    {
        var repository = httpContext.GetService<IRepository>();
        return repository.DbSet<AccountEntity>()
                         .SingleOrDefault(w => w.Id == userId);
    }
}