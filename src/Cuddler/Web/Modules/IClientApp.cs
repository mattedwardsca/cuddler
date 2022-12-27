using Microsoft.AspNetCore.Http;

namespace Cuddler.Web.Modules;

public interface IClientApp : IApp
{
    Task<List<IMenuItem>> GetAppMenu(HttpContext context);
}