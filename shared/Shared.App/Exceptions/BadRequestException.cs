namespace Shared.App.Exceptions;

public sealed class BadRequestException : ApplicationLogicException
{
    public BadRequestException(string? message) : base(message) { }
}