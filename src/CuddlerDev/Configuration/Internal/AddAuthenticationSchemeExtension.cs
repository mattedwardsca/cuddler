using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CuddlerDev.Configuration.Internal;

internal static class AddAuthenticationSchemeExtension
{
    public static void AddAuthenticationScheme<TDbContext, TUser>(this WebApplicationBuilder builder, ApplicationSettings appSettings) where TDbContext : IdentityDbContext<TUser> where TUser : IdentityUser
    {
        builder.Services.Configure<IdentityOptions>(options => {
            // Default Password settings
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });

        builder.Services.AddIdentity<TUser, IdentityRole>(config => {
                   config.User.RequireUniqueEmail = false;
                   config.SignIn.RequireConfirmedEmail = true;
                   config.Password.RequireDigit = false;
                   config.Password.RequiredLength = 6;
                   config.Password.RequiredUniqueChars = 1;
                   config.Password.RequireLowercase = false;
                   config.Password.RequireNonAlphanumeric = false;
                   config.Password.RequireUppercase = false;
               })
               .AddEntityFrameworkStores<TDbContext>()
               .AddSignInManager<SignInManager<TUser>>()
               .AddUserManager<UserManager<TUser>>()
               .AddRoleManager<RoleManager<IdentityRole>>()
               .AddDefaultTokenProviders();

        // IDENTITY
        builder.Services.ConfigureApplicationCookie(options => {
            options.Cookie.Name = ".AspNet.SharedCookie";
            options.Cookie.Path = "/";
            if (!string.IsNullOrEmpty(appSettings.Subdomain))
            {
                options.Cookie.Domain = appSettings.Subdomain;
            }

            options.LoginPath = "/Identity/Login";
            options.LogoutPath = "/Identity/Logout";
            options.AccessDeniedPath = "/Identity/AccessDenied";
        });

        // COOKIES
        var cookieFolder = Path.Combine(appSettings.ContentRootFolder!, "Cookies");
        var directoryInfo = new DirectoryInfo(cookieFolder);
        if (!Directory.Exists(cookieFolder))
        {
            directoryInfo.Create();
        }

        builder.Services.AddDataProtection()
               .PersistKeysToFileSystem(directoryInfo)
               .SetApplicationName("SharedCookieApp"); // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/Configuration/overview?view=aspnetcore-3.1
    }
}