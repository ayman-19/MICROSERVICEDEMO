namespace Ordering.Application.Exceptions;

public sealed class ServiceException : Exception
{
    public ServiceException(string message)
        : base(message) { }

    public ServiceException() { }

    public ServiceException(string message, Exception innerException)
        : base(message, innerException) { }
}
