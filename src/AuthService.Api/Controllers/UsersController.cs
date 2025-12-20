using AuthService.Application.UseCases.Users.Register;
using AuthService.Communication.Requests;
using AuthService.Communication.Responses;
using AuthService.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody] RequestUserJson request,
        [FromServices] IRegisterUserUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
