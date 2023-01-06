using Cuddler.Data.Entities;
using Cuddler.Ui;

namespace Cuddler.Core.Ecommerce;

public interface IOrderType
{
    string Id { get; }

    string ItemName { get; }

    string Name { get; }

    string OrderTemplate { get; }

    CuddlerForm<TransactionEntity> GetCreateModel();
}