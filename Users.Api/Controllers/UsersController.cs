using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Credentials.Commands;

namespace Users.Api.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : Controller
{

    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddCredentials(AddCredentialsCommand command)
    {
        return await _mediator.Send(command);
    }
}