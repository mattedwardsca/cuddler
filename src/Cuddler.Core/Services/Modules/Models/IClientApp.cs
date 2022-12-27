using Microsoft.AspNetCore.Http;

namespace Cuddler.Core.Services.Modules.Models;

public interface IClientApp : IApp
{
    Task<List<IMenuItem>> GetAppMenu(HttpContext context);
}