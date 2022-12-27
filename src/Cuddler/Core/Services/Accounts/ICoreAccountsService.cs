using Cuddler.Data.Entities;
using Cuddler.Web.Identity;

namespace Cuddler.Core.Services.Accounts;

public interface ICoreAccountsService
{
    Task<AccountEntity?> CreateAccount(string linkId, string password);

    AccountRequestEntity? GetRequestByEmail(string email);

    IQueryable<AccountEntity> ListActiveClients(OrganizationEntity organization);

    IQueryable<AccountEntity> ListArchivedAccounts();

    IQueryable<AccountEntity> ListArchivedAccountsByRole(EProfileRole? eRole);

    IQueryable<AccountEntity> ListEmployeesByRole(OrganizationEntity organizationEntity, EProfileRole eRole);

    AccountEntity RegisterClient(string organizationId, string name, string? accountNumber, string email, string? phoneNumber, EProfileRole eRole, bool isOrganizationAdmin);

    AccountRequestEntity GetRequestById(string id);

    AccountRequestEntity CreateRequest(string email, string organizationName, string? phoneNumber, string name);

    IList<AccountRequestEntity> ListActiveInvitations(OrganizationEntity organization);
}