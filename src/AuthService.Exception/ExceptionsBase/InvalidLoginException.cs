
using System.Net;

namespace AuthService.Exception.ExceptionsBase;

public class InvalidLoginException : AuthServiceException
{
    public InvalidLoginException() : base("Email and/or password inválidos.")
    {
        
    }
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
