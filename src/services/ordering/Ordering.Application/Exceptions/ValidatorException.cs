namespace Ordering.Application.Exceptions;

public sealed class ValidatorException : Exception
{
    public ValidatorException(string message)
        : base(message) { }

    public ValidatorException() { }

    public ValidatorException(string message, Exception innerException)
        : base(message, innerException) { }
}
