using Api.Controllers.BaseController;
using CleanArchitecture.Application.Credentials.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
public class CredentialsController : BaseController.BaseController
{
    public CredentialsController()
    {

    }

    [HttpPost]
    public async Task<ActionResult<int>> AddCredentials(AddCredentialsCommand command)
    {
        return await Mediator.Send(command);
    }
}
