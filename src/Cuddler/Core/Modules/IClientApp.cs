using Microsoft.AspNetCore.Http;

namespace Cuddler.Core.Modules;

public interface IClientApp : IApp
{
    Task<List<IMenuItem>> GetAppMenu(HttpContext context);
}