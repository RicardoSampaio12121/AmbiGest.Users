﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Users.Commands;
using Users.Application.Users.Queries;
using Users.Domain.Entities;

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

    [HttpPost("createUser")]
    public async Task<ActionResult<User>> CreateUser(AddUserCommand command)
    {
        var output = await _mediator.Send(command);
        return Ok(output);
    }

    //TODO: It needs to accept a JWT token in order to work
    //TODO: It needs to actually validate the jwt token
    [HttpPut]
    public async Task<ActionResult<int>> UpdateUser(UpdateUserCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    //TODO: It needs to accept a JWT token in order to work
    //TODO: It needs to actually validate the jwt token
    [HttpPut("updateEmail")]
    public async Task<ActionResult<int>> UpdateEmail(UpdateEmailCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("deleteUser")]
    public async Task<ActionResult<int>> DeleteUser(DeleteUserCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
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