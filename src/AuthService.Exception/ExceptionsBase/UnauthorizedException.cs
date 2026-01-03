using System.Net;

namespace AuthService.Exception.ExceptionsBase;

public class UnauthorizedException : AuthServiceException
{
    public UnauthorizedException() : base("Unauthorized.")
    {
    }
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
