using Cuddler.Core.Models;

namespace Cuddler.Core.Identity;

public interface IProfile : IData, IHasName
{
    IOrganization? GetOrganization();
}