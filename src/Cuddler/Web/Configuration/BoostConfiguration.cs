using Cuddler.Core.Services.Settings;
using Microsoft.Extensions.Logging;

namespace Cuddler.Web.Configuration;

public sealed class BoostConfiguration
{
    public GlobalSettings GlobalSettings { get; set; } = new();

    /// <summary>
    ///     If true, the database is reset each time the application restarts
    /// </summary>
    public bool ResetDbOnStartup { get; set; }

    public LogLevel LogLevel { get; set; } = LogLevel.Information;

    public IApplicationServices? StartupServices { get; set; }
}