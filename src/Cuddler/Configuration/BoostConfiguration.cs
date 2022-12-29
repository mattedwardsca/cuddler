using Microsoft.Extensions.Logging;

namespace Cuddler.Configuration;

public sealed class BoostConfiguration
{
    /// <summary>
    ///     If true, the database is reset each time the application restarts
    /// </summary>
    public bool ResetDbOnStartup { get; set; }

    public LogLevel LogLevel { get; set; } = LogLevel.Information;

    public IApplicationServices? StartupServices { get; set; }
}