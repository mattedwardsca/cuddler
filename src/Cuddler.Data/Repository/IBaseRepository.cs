using Cuddler.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cuddler.Data.Repository;

public interface IBaseRepository
{
    DbSet<AccountRequestEntity> AccountRequests { get; set; }

    DbSet<AccountEntity> Accounts { get; set; }

    DbSet<AddressEntity> Addresses { get; set; }

    DbSet<DocumentEntity> Documents { get; set; }

    DbSet<EmailEntity> Emails { get; set; }

    DbSet<EmailTemplateEntity> EmailTemplates { get; set; }

    DbSet<FieldEntity> Fields { get; set; }

    DbSet<OptionEntity> Options { get; set; }

    DbSet<OrderEntity> Orders { get; set; }

    DbSet<OrganizationEntity> Organizations { get; set; }

    DbSet<ProductEntity> Products { get; set; }

    DbSet<ProfileEntity> Profiles { get; set; }

    DbSet<ProjectEntity> Projects { get; set; }

    DbSet<ScheduleEntity> Schedules { get; set; }

    DbSet<SettingEntity> Settings { get; set; }

    DbSet<TransactionEntity> Transactions { get; set; }
}