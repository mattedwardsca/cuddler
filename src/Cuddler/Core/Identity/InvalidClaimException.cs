namespace Cuddler.Core.Identity;

public class InvalidClaimException : Exception
{
    public InvalidClaimException(string? message) : base(message)
    {
    }
}