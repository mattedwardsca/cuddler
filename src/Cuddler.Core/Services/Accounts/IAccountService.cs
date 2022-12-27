using Cuddler.Core.Helpers;
using Cuddler.Core.Services.Accounts.Models;
using Cuddler.Data.Entities;

namespace Cuddler.Core.Services.Accounts;

public interface IAccountService
{
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
    AccountEntity GetAccountById(string accountId);
}