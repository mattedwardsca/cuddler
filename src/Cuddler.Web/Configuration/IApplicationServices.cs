using Microsoft.AspNetCore.Builder;

namespace Cuddler.Web.Configuration;

public interface IApplicationServices
{
    void InitProgramStartup(WebApplicationBuilder applicationBuilder);
}