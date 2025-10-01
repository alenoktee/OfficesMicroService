namespace OfficesMicroService.Application.Exceptions;

public class DuplicateAddressException : Exception
{
    public DuplicateAddressException(string message) : base(message) { }
}
