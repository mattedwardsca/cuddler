using Microsoft.AspNetCore.Http;

namespace CuddlerDev.Modules;

public interface IApp
{
    bool Hidden { get; set; }

    string? Icon { get; set; }

    string Id { get; }

    bool IsForAdmins { get; set; }

    bool IsForClients { get; set; }

    string Name { get; set; }

    string? Description { get; set; }

    Task<List<IMenuItem>> GetAppMenu(HttpContext context);
}