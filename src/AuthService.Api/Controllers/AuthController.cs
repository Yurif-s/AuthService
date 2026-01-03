using AuthService.Application.UseCases.Auth.Refresh;
using AuthService.Communication.Requests;
using AuthService.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace AuthService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(ResponseRefreshJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(
        [FromBody] RequestRefreshJson request,
        [FromServices] IRefreshTokenUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
