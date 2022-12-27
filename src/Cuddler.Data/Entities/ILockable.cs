namespace Cuddler.Data.Entities;

public interface ILockable : IData
{
    DateTime? DateLocked { get; set; }

    bool IsLocked();
}