namespace Cuddler.Data.Entities;

public interface IProfile : IData, IHasName
{
    OrganizationEntity? Organization { get; set; }
}