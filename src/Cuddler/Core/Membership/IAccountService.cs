using Cuddler.Data.Entities;
using Cuddler.Web.Helpers;

namespace Cuddler.Core.Membership;

public interface IAccountService
{
    Task<AccountEntity?> CreateAccount(string linkId, string password);

    AccountRequestEntity? GetRequestByEmail(string email);

    IQueryable<AccountEntity> ListActiveClients(OrganizationEntity organization);

    IQueryable<AccountEntity> ListArchivedAccounts();

    IQueryable<AccountEntity> ListArchivedAccountsByRole(EProfileRole? eRole);

    IQueryable<AccountEntity> ListEmployeesByRole(OrganizationEntity organizationEntity, EProfileRole eRole);

    AccountEntity RegisterClient(string organizationId, string name, string? accountNumber, string email, string? phoneNumber, EProfileRole eRole, bool isOrganizationAdmin);

    AccountRequestEntity GetAccountRequestById(string id);

    AccountRequestEntity CreateRequest(string email, string organizationName, string? phoneNumber, string name);

    IList<AccountRequestEntity> ListActiveInvitations(OrganizationEntity organization);

    int CountAccountsMissingDeliveryAddresses();

    int CountAccountsMissingPhoneNumbers();

    int CountAccountsWithUnconfirmedEmails();

    int CountAccountsWithUnconfirmedPhones();

    List<AccountEntity> ListAccountsMissingDeliveryAddresses();

    IQueryable<AccountEntity> ListAccountsMissingPhoneNumbers();

    IQueryable<AccountEntity> ListAccountsWithUnconfirmedEmails();

    IQueryable<AccountEntity> ListAccountsWithUnconfirmedPhones();

    IQueryable<AccountEntity> ListActiveAccounts();

    IQueryable<AccountEntity> ListActiveEmployees(OrganizationEntity organization);

    IQueryable<AccountEntity> ListDisabledAccounts();

    IQueryable<AccountEntity> ListDisabledClients(OrganizationEntity organizationEntity);

    IQueryable<AccountEntity> ListLockedClients(OrganizationEntity organizationEntity);

    List<AccountEntity> ListLockedEmployees(OrganizationEntity organizationEntity);

    IQueryable<AccountEntity> ListUsersNotInThisRole(EProfileRole role);

    void LockUserAccount(string accountId);

    AccountEntity RegisterEmployee(OrganizationEntity organization, CreateEmployeeModel model);

    void SaveAccountLanguage(string accountId, string language);

    void SaveUserLanguage(string userId, string language);

    IQueryable<AccountEntity> SearchUsers(string text);

    EmailEntity SendEmail_AccountRequestChangePassword(AccountsResetPasswordRequestModel model);
    Task<AccountEntity> GetAccountById(string accountId);

    Task ArchiveAccount(string id);

    Task SetPassword(UserPasswordModel model);
    
    Task<AccountEntity?> FindByAccountNumber(string accountNumber);
    
    Task<AccountEntity?> FindByEmail(string modelEmail);
    
    Task RestoreAccount(string accountId);
}