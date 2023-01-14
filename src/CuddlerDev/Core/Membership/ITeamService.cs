using CuddlerDev.Data.Entities;

namespace CuddlerDev.Core.Membership;

public interface ITeamService
{
    IQueryable<AccountEntity> ListMyTeamMembers(AccountEntity account);
    IQueryable<AccountEntity> ListMySupervisors(AccountEntity account);
    IEnumerable<AccountEntity> ListAllMyTeamMembers(string userId);
}