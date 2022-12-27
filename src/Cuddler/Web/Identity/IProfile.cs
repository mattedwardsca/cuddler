using Cuddler.Data.Entities;

namespace Cuddler.Web.Identity;

public interface IProfile : IData, IHasName
{
    OrganizationEntity? Organization { get; set; }
}