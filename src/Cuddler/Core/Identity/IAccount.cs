using Cuddler.Core.Models;

namespace Cuddler.Core.Identity;

public interface IAccount : IData
{
    string Email { get; set; }

    bool IsAdmin { get; set; }

    string UserName { get; set; }

    IProfile GetProfile();
}