
using System.Net;

namespace AuthService.Exception.ExceptionsBase;

public class NotFoundException : AuthServiceException
{
    private readonly string _error;
    public NotFoundException(string errorMessage) : base(string.Empty)
    {
        _error = errorMessage;
    }
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors() => [_error];
}
