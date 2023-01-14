using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Users.Commands;
using Users.Application.Users.Queries;

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

    //TODO: It needs to accept a JWT token in order to work
    //TODO: It needs to actually validate the jwt token
    [HttpPut("updateEmail")]
    public async Task<ActionResult<int>> UpdateEmail(UpdateEmailCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("deleteUser")]
    public async Task<ActionResult<int>> DeleteUser(DeleteUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet("getUser")]
    public async Task<ActionResult<int>> GetUser(GetUserQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet("getAllUsers")]
    public async Task<ActionResult<int>> GetAllUsers(GetAllUsersQuery query)
    {
        return await _mediator.Send(query);
    }
}