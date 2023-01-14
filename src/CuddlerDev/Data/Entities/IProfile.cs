namespace CuddlerDev.Data.Entities;

public interface IProfile : IData, IHasName
{
    OrganizationEntity? Organization { get; set; }
}