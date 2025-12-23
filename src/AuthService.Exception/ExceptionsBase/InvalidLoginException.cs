
using System.Net;

namespace AuthService.Exception.ExceptionsBase;

public class InvalidLoginException : AuthServiceException
{
    public InvalidLoginException() : base("Invalid email and/or password.")
    {
        
    }
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
