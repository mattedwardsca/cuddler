namespace Cuddler.Data.Entities;

public interface ICard
{
    string Id { get; set; }

    string Name { get; set; }

    string? Status { get; set; }

    string? BoardId { get; set; }
}