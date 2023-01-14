using CuddlerDev.Data.Entities;
using CuddlerDev.Ui;

namespace CuddlerDev.Core.Ecommerce;

public interface IOrderType
{
    string Id { get; }

    string ItemName { get; }

    string Name { get; }

    string OrderTemplate { get; }

    CuddlerForm<TransactionEntity> GetCreateModel();
}