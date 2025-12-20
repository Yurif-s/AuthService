namespace AuthService.Exception.ExceptionsBase;

public abstract class AuthServiceException : SystemException
{
    protected AuthServiceException(string message) : base(message)
    {
    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
