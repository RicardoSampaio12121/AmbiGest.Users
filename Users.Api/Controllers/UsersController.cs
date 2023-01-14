using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Users.Commands;

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
    public async Task<ActionResult<int>> CreateUser(AddUserCommand command)
    {
        return await _mediator.Send(command);
    }

    //TODO: It needs to accept a JWT token in order to work
    //TODO: It needs to actually validate the jwt token
    [HttpPut]
    public async Task<ActionResult<int>> UpdateUser(UpdateUserCommand command)
    {
        return await _mediator.Send(command);
    }
}