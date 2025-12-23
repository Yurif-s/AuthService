using AuthService.Communication.Responses;
using AuthService.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthService.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AuthServiceException)
        {
            HandleApplicationException(context);
        }
        else
            ThrowInternalServerError(context);
    }

    private void HandleApplicationException(ExceptionContext context)
    {
        var authServiceException = (AuthServiceException)context.Exception;

        var errorResponse = new ResponseErrorMessageJson(authServiceException.GetErrors());

        context.HttpContext.Response.StatusCode = authServiceException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowInternalServerError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorMessageJson("Internal Server Error.");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
