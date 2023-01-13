using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BaseController;

[ApiController]
[Route("/api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private ISender _mediator = null;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

}
