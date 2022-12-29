using Microsoft.AspNetCore.Builder;

namespace Cuddler.Configuration;

public interface IApplicationServices
{
    void InitProgramStartup(WebApplicationBuilder applicationBuilder);
}