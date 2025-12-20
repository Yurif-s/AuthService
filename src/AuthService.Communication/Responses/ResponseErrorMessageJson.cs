namespace AuthService.Communication.Responses;

public class ResponseErrorMessageJson
{
    public List<string> ErrorMessages { get; set; }

    public ResponseErrorMessageJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
    public ResponseErrorMessageJson(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }
}
