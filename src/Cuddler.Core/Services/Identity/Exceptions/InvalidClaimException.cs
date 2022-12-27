namespace Cuddler.Core.Services.Identity.Exceptions;

public class InvalidClaimException : Exception
{
    public InvalidClaimException(string? message) : base(message)
    {
    }
}