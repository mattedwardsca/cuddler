using Microsoft.AspNetCore.Builder;

namespace CuddlerDev.Configuration;

public interface IApplicationServices
{
    void InitProgramStartup(WebApplicationBuilder applicationBuilder);
}