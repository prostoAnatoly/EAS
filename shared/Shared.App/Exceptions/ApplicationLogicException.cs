namespace Shared.App.Exceptions;

public abstract class ApplicationLogicException : Exception
{
    public ApplicationLogicException(string? message) : base(message) { }

}
