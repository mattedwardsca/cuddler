namespace Cuddler.Data.Entities;

public interface IAccount : IData
{
    string Email { get; set; }

    bool IsAdmin { get; set; }

    string UserName { get; set; }

    IProfile GetProfile();
}