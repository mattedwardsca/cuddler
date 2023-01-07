using Cuddler.Data.Entities;

namespace Cuddler.Core.Membership;

public interface ITeamService
{
    IQueryable<AccountEntity> ListMyTeamMembers(AccountEntity account);
    IQueryable<AccountEntity> ListMySupervisors(AccountEntity account);
    IEnumerable<AccountEntity> ListAllMyTeamMembers(string userId);
}